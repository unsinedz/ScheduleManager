var Framework = Framework || {};
Framework.Autocomplete = (function ($) {
    var typeMappers = {};

    // override image source
    M.Autocomplete.prototype._renderDropdown = function (data, val) {
        var _this39 = this;

        this._resetAutocomplete();

        var matchingData = [];

        // Gather all matching data
        for (var key in data) {
            if (data.hasOwnProperty(key) && key.toLowerCase().indexOf(val) !== -1) {
                // Break if past limit
                if (this.count >= this.options.limit) {
                    break;
                }

                var entry = {
                    data: data[key],
                    key: key
                };
                matchingData.push(entry);

                this.count++;
            }
        }

        // Sort
        if (this.options.sortFunction) {
            var sortFunctionBound = function (a, b) {
                return _this39.options.sortFunction(a.key.toLowerCase(), b.key.toLowerCase(), val.toLowerCase());
            };
            matchingData.sort(sortFunctionBound);
        }

        // Render
        for (var i = 0; i < matchingData.length; i++) {
            var _entry = matchingData[i];
            var $autocompleteOption = $('<li></li>');
            // put original entry to be used on click
            $autocompleteOption.data('entry', _entry);
            // using imageSrc instead of direct data
            if (_entry.data && _entry.data.imageSrc)
                $autocompleteOption.append("<img src=\"" + _entry.data.imageSrc + "\" class=\"right circle\"><span>" + _entry.key + "</span>");
            else
                $autocompleteOption.append('<span>' + _entry.key + '</span>');

            $(this.container).append($autocompleteOption);
            this._highlight(val, $autocompleteOption);
        }
    };

    M.Autocomplete.prototype.selectOption = function (el) {
        var text = el.text().trim();
        // use entry data instead of text for autocomplete
        var entry = $(el).data('entry');
        var data = entry === undefined ? undefined : entry.data;
        var key = entry === undefined ? undefined : entry.key;
        this.el.value = '';
        this.$el.trigger('change', data);
        this._resetAutocomplete();
        this.close();
        // Handle onAutocomplete callback.
        if (typeof this.options.onAutocomplete === 'function')
            this.options.onAutocomplete.call(this, data, key);
    };

    function AutocompleteOptions(url, limit, allowMultiple, autocompleteType, autocompleteData) {
        this.url = url;
        this.limit = limit;
        this.allowMultiple = allowMultiple;
        this.autocompleteType = autocompleteType;
        this.autocompleteData = autocompleteData;
    }
    AutocompleteOptions.fromElement = function ($element) {
        return new AutocompleteOptions($element.attr('data-autocomplete-url'),
            $element.attr('data-autocomplete-limit'),
            $element.attr('data-autocomplete-multiple').toLowerCase() === 'true',
            $element.attr('data-autocomplete-type'),
            $element.attr('data-autocomplete-data'));
    };

    function init(selector) {
        var $objects = $(selector);
        if (!$objects.length)
            return;

        $objects.each(function () {
            var $el = $(this);
            $el.data('name', $el.attr('name'));
            $el.closest('form').on('submit', function () {
                if ($(this).valid())
                    $el.removeAttr('name');
            });

            var autocompleteMapper, chipMapper, chipSuffix;
            var options = AutocompleteOptions.fromElement($el);
            if (options.autocompleteType) {
                var typeMapper = typeMappers[options.autocompleteType];
                if (typeMapper) {
                    autocompleteMapper = autocompleteMapper || typeMapper.autocomplete;
                    chipMapper = chipMapper || typeMapper.chip;
                    chipSuffix = chipSuffix || typeMapper.chipSuffix;
                }
            }

            if (options.autocompleteData) {
                options.autocompleteData = JSON.parse(options.autocompleteData);
                if (!Array.isArray(options.autocompleteData))
                options.autocompleteData = [options.autocompleteData];
            }

            if (options.url && options.url.length) {
                $.get(options.url).done(function (data) {
                    var autocompleteData = [],
                        valueData = [];
                    if (Array.isArray(options.autocompleteData) && options.autocompleteData.length) {
                        $.each(data, function () {
                            var isValueData = false;
                            var _data = this;
                            $.each(options.autocompleteData, function () {
                                if (JSON.stringify(_data) === JSON.stringify(this)) {
                                    isValueData = true;
                                    return false;
                                }
                            });
                            if (isValueData && (options.allowMultiple || valueData.length <= 1))
                                valueData.push(_data);
                            else
                                autocompleteData.push(_data);
                        });
                    }
                    else
                        autocompleteData = data;

                    var useAutocompleteMapper = typeof autocompleteMapper === 'function';
                    if (useAutocompleteMapper) {
                        autocompleteData = autocompleteMapper(autocompleteData);
                        valueData = autocompleteMapper(valueData);
                    }

                    var $chips = $('<div></div>');
                    $chips.insertAfter($el);
                    $.each(valueData, function () {
                        var autocompleteItem = this;
                        if (useAutocompleteMapper)
                            autocompleteItem = autocompleteMapper(autocompleteItem);

                        handleAutocomplete($el, autocompleteItem.data, autocompleteItem.key, chipMapper, chipSuffix, autocompleteData, options, $chips);
                    });
                    $el.autocomplete({
                        limit: options.limit,
                        data: autocompleteData,
                        onAutocomplete: function (data, key) {
                            handleAutocomplete($el, data, key, chipMapper, chipSuffix, autocompleteData, options, $chips);
                        }
                    });
                });
            }
        });
    }

    function handleAutocomplete($element, data, key, chipMapper, chipSuffix, autocompleteData, autocompleteOptions,
        $chips) {
        var title = data;
        var value = data;
        if (typeof chipMapper === 'function') {
            var chipData = chipMapper(data);
            title = chipData.title;
            value = chipData.value;
        }

        var chipIndex = undefined;
        if (autocompleteOptions.allowMultiple) {
            chipIndex = $chips.children().length;
            chipSuffix = '[' + chipIndex + ']' + chipSuffix;
        }

        var $chip = $(makeChip(title, value, $element.data('name') + chipSuffix, chipIndex));
        $chip.find('.close').click(function () {
            autocompleteData[key] = data;
            removeElementAutocompleteData($element, key);
            if (!autocompleteOptions.allowMultiple)
                $element.prop('disabled', false);

            $element.trigger('change');
            $element.valid();
        });

        $chips.append($chip);
        delete autocompleteData[key];
        addElementAutocompleteData($element, key, data);
        if (!autocompleteOptions.allowMultiple)
            $element.prop('disabled', true);

        $element.valid();
    }

    function addElementAutocompleteData($element, key, data) {
        var autocompleteValue = $element.data('autocompleteValue');
        autocompleteValue = autocompleteValue || {};
        autocompleteValue[key] = data;
        $element.data('autocompleteValue', autocompleteValue);
    }

    function removeElementAutocompleteData($element, key) {
        var autocompleteValue = $element.data('autocompleteValue');
        autocompleteValue = autocompleteValue || {};
        delete autocompleteValue[key];
        $element.data('autocompleteValue', autocompleteValue);
    }

    function makeChip(title, value, inputName, valueIndex) {
        var indexInput = '';
        if (valueIndex !== undefined)
            indexInput = '<input type="hidden" name="' + inputName.replace(/(\.[^\.]+$)/g, '.Index') + '" value="' + valueIndex + '" />';

        return '<div class="chip">' +
            title +
            indexInput +
            '<input type="hidden" name="' + inputName + '" value="' + value + '" />' +
            '<i class="close material-icons"> close</i>' +
            '</div>';
    }

    function addTypeMapper(typeName, autocompleteMapper, chipMapper, chipSuffix) {
        typeMappers[typeName] = {
            autocomplete: autocompleteMapper,
            chip: chipMapper,
            chipSuffix: chipSuffix
        };
    }

    function addTypeMappers(typeNames, autocompleteMapper, chipMapper, chipSuffix) {
        if (typeNames && typeNames.length) {
            $.each(typeNames, function () {
                if (this !== undefined)
                    addTypeMapper(this, autocompleteMapper, chipMapper, chipSuffix);
            });
        }
    }

    $(function () {
        init('[data-autocomplete="true"]');
    });

    return {
        init: init,
        addTypeMapper: addTypeMapper,
        addTypeMappers: addTypeMappers
    };
})(jQuery);