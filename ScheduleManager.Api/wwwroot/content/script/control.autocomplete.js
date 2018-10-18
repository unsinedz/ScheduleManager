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

            var options = AutocompleteOptions.fromElement($el);
            if (options.url && options.url.length) {
                $.get(options.url).done(function (data) {
                    var autocompleteData = data;
                    if (typeof autocompleteMapper === 'function')
                        autocompleteData = autocompleteMapper(data);

                    var $chips = $('<div></div>');
                    $chips.insertAfter($el);
                    $el.autocomplete({
                        limit: options.limit,
                        data: autocompleteData,
                        onAutocomplete: function (data, key) {
                            var title = data;
                            var value = data;
                            if (typeof chipMapper === 'function') {
                                var chipData = chipMapper(data);
                                title = chipData.title;
                                value = chipData.value;
                            }

                            var $chip = $(makeChip(title, value, $el.prop('name') + chipSuffix));
                            $chip.find('.close').click(function () {
                                autocompleteData[key] = data;
                                removeElementAutocompleteData($el, key);
                                if (!options.allowMultiple)
                                    $el.prop('readonly', false);

                                $el.trigger('change');
                                $el.valid();
                            });

                            $chips.append($chip);
                            delete autocompleteData[key];
                            addElementAutocompleteData($el, key, data);
                            if (!options.allowMultiple)
                                $el.prop('readonly', true);

                            $el.valid();
                        }
                    });
                });
            }
        });
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