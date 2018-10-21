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
            if (!!_entry.data.imageSrc) {
                $autocompleteOption.append("<img src=\"" + _entry.data.imageSrc + "\" class=\"right circle\"><span>" + _entry.key + "</span>");
            } else {
                $autocompleteOption.append('<span>' + _entry.key + '</span>');
            }

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

    function AutocompleteOptions(url, limit, allowMultiple) {
        this.url = url;
        this.limit = limit;
        this.allowMultiple = allowMultiple;
    }
    AutocompleteOptions.fromElement = function ($element) {
        return new AutocompleteOptions($element.attr('data-autocomplete-url'),
            $element.attr('data-autocomplete-limit'),
            $element.attr('data-autocomplete-multiple').toLowerCase() === 'true');
    };

    function init(selector, chipSuffix, autocompleteMapper, chipMapper) {
        var $objects = $(selector);
        if (!$objects.length)
            return;

        $objects.each(function () {
            var $el = $(this);
            var autocompleteType = $el.attr('data-autocomplete-type');
            if (autocompleteType && autocompleteType.length) {
                var typeMapper = typeMappers[autocompleteType];
                if (typeMapper) {
                    autocompleteMapper = autocompleteMapper || typeMapper.autocomplete;
                    chipMapper = chipMapper || typeMapper.chip;
                    chipSuffix = chipSuffix || typeMapper.chipSuffix;
                }
            }

            var elementData = $el.attr('data-autocomplete-data');
            if (elementData !== undefined)
                elementData = JSON.parse(elementData);

            if (!Array.isArray(elementData))
                elementData = [elementData];

            var options = AutocompleteOptions.fromElement($el);
            if (options.url && options.url.length) {
                $.get(options.url).done(function (data) {
                    if (typeof autocompleteMapper === 'function')
                        data = autocompleteMapper(data);

                    var autocompleteData = [],
                        valueData = [];
                    if (Array.isArray(elementData) && elementData.length) {
                        $.each(data, function () {
                            var isValueData = false;
                            var _data = this;
                            $.each(elementData, function () {
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

                    var $chips = $('<div></div>');
                    $chips.insertAfter($el);
                    $.each(valueData, function () {
                        var autocompleteItem = autocompleteMapper(this);
                        handleAutocomplete($el, autocompleteItem.key, autocompleteItem.data, chipMapper, chipSuffix, autocompleteData, options, $chips);
                    });
                    $el.autocomplete({
                        limit: options.limit,
                        data: autocompleteData,
                        onAutocomplete: function (data, key) {
                            handleAutocomplete($el, data, key, chipMapper, chipSuffix, autocompleteData, options, $ships);
                        }
                    });
                });
            }
        });
    }

    function handleAutocomplete($element, key, data, chipMapper, chipSuffix, autocompleteData, autocompleteOptions,
        $chips) {
        var title = data;
        var value = data;
        if (typeof chipMapper === 'function') {
            var chipData = chipMapper(data);
            title = chipData.title;
            value = chipData.value;
        }

        if (autocompleteOptions.allowMultiple)
            chipSuffix = '[' + $chips.children().length + ']' + chipSuffix;

        var $chip = $(makeChip(title, value, $element.prop('name') + chipSuffix));
        $chip.find('.close').click(function () {
            autocompleteData[key] = data;
            removeElementAutocompleteData($element, key);
            if (!autocompleteOptions.allowMultiple)
                $element.prop('readonly', false);

            $element.trigger('change');
            $element.valid();
        });

        $chips.append($chip);
        delete autocompleteData[key];
        addElementAutocompleteData($element, key, data);
        if (!autocompleteOptions.allowMultiple)
            $element.prop('readonly', true);

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

    function makeChip(title, value, inputName) {
        return '<div class="chip">' +
            title +
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

    $(document).ready(function () {
        init('[data-autocomplete="true"]');
    });

    return {
        init: init,
        addTypeMapper: addTypeMapper
    };
})(jQuery);