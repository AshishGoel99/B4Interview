// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your Javascript code.

$(document).ready(function () {

    //restoring values back to fields after navigation
    if (location.search && location.pathname) {
        $("#searchCtrl").val(decodeURI(location.search.split('search=')[1]));
        $("#searchType").val(getSearchType(location.pathname.split('/')[1]));
    }

    var data = new Bloodhound({
        datumTokenizer: Bloodhound.tokenizers.obj.whitespace('value'),
        queryTokenizer: Bloodhound.tokenizers.whitespace,
        remote: {
            url: '/CompanyOverview?handler=NamesAndPosition&query=%QUERY',
            wildcard: '%QUERY'
        }
    });

    $('.typeahead').typeahead({
        hint: true,
        highlight: true,
        minLength: 3
    },
        {
            source: data
        });
});

function formSubmit() {
    event.preventDefault();

    var companyName = $("#searchCtrl").val();
    var searchType = $("#searchType").val();

    window.location.href = "/" + getModelName(searchType) + "?search=" + companyName;
}

function getModelName(searchType) {
    switch (searchType) {
        case "Reviews":
        default:
            return "Review";
        case "Interviews":
            return "Interview";
    }
}

function getSearchType(model) {
    switch (model) {
        case "Review":
        default:
            return "Reviews";
        case "Interview":
            return "Interviews";

    }
}