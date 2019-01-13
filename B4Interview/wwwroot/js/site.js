// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {

    $('.alert').alert();

    //restoring values back to fields after navigation
    if (location.search && location.search.indexOf('search=') >= 0 && location.pathname) {
        $("#searchCtrl").val(decodeURI(location.search.split('search=')[1]));
        $("#searchType").val(getSearchType(location.pathname.split('/')[1]));
    }

    $('.typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 3
    }, {
        source: function (query, syncCb, asyncCb) {
            $.getJSON('/CompanyOverview?handler=NamesAndPosition&type=' + $("#searchType").val() + '&query=' + query, function (data) {
                console.log(data);
                asyncCb(data);
            })
        }
    });
});

function formSubmit() {
    event.preventDefault();

    var query = $("#searchCtrl").val();
    var searchType = $("#searchType").val();

    window.location.href = "/" + getModelName(searchType) + "?search=" + query;
}

function getModelName(searchType) {
    switch (searchType) {
        case "Reviews":
        default:
            return "Review";
        case "Interviews":
            return "Interview";
        case "Jobs":
            return "ReferralJob";
    }
}

function getSearchType(model) {
    switch (model) {
        case "Review":
        default:
            return "Reviews";
        case "Interview":
            return "Interviews";
        case "ReferralJob":
            return "Jobs";

    }
}


////////////////////////////////////////////////////////////////// STAR PLUGIN //////////////////////////////////////////////////////

// Starrr plugin (https://github.com/dobtco/starrr)
var __slice = [].slice;

(function ($, window) {
    var Starrr;

    Starrr = (function () {
        Starrr.prototype.defaults = {
            rating: void 0,
            numStars: 5,
            change: function (e, value) { }
        };

        function Starrr($el, options) {
            var i, _, _ref,
                _this = this;

            this.options = $.extend({}, this.defaults, options);
            this.$el = $el;
            _ref = this.defaults;
            for (i in _ref) {
                _ = _ref[i];
                if (this.$el.data(i) != null) {
                    this.options[i] = this.$el.data(i);
                }
            }
            this.createStars();
            this.syncRating();
            this.$el.on('mouseover.starrr', 'span', function (e) {
                return _this.syncRating(_this.$el.find('span').index(e.currentTarget) + 1);
            });
            this.$el.on('mouseout.starrr', function () {
                return _this.syncRating();
            });
            this.$el.on('click.starrr', 'span', function (e) {
                return _this.setRating(_this.$el.find('span').index(e.currentTarget) + 1);
            });
            this.$el.on('starrr:change', this.options.change);
        }

        Starrr.prototype.createStars = function () {
            var _i, _ref, _results;

            _results = [];
            for (_i = 1, _ref = this.options.numStars; 1 <= _ref ? _i <= _ref : _i >= _ref; 1 <= _ref ? _i++ : _i--) {
                _results.push(this.$el.append("<span class='far fa-star'></span>"));
            }
            return _results;
        };

        Starrr.prototype.setRating = function (rating) {
            if (this.options.rating === rating) {
                rating = void 0;
            }
            this.options.rating = rating;
            this.syncRating();
            return this.$el.trigger('starrr:change', rating);
        };

        Starrr.prototype.syncRating = function (rating) {
            var i, _i, _j, _ref;

            rating || (rating = this.options.rating);
            if (rating) {
                for (i = _i = 0, _ref = rating - 1; 0 <= _ref ? _i <= _ref : _i >= _ref; i = 0 <= _ref ? ++_i : --_i) {
                    this.$el.find('span').eq(i).removeClass('far fa-star').addClass('fas fa-star');
                }
            }
            if (rating && rating < 5) {
                for (i = _j = rating; rating <= 4 ? _j <= 4 : _j >= 4; i = rating <= 4 ? ++_j : --_j) {
                    this.$el.find('span').eq(i).removeClass('fas fa-star').addClass('far fa-star');
                }
            }
            if (!rating) {
                return this.$el.find('span').removeClass('fas fa-star').addClass('far fa-star');
            }
        };

        return Starrr;

    })();
    return $.fn.extend({
        starrr: function () {
            var args, option;

            option = arguments[0], args = 2 <= arguments.length ? __slice.call(arguments, 1) : [];
            return this.each(function () {
                var data;

                data = $(this).data('star-rating');
                if (!data) {
                    $(this).data('star-rating', (data = new Starrr($(this), option)));
                }
                if (typeof option === 'string') {
                    return data[option].apply(data, args);
                }
            });
        }
    });
})(window.jQuery, window);
/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////