    $('select').on('change', function (e) {
            var optionSelected = $("option:selected", this);
    var valueSelected = this.value;
    $("#airtport td.country:contains('" + valueSelected + "')").parent().show();
    $("#airtport td.country:not(:contains('" + valueSelected + "'))").parent().hide();
        sortTable($("#airtport"), 0, "asc");
    });

$(function () {
    var myTable = "#airtport";
    var myTableBody = myTable + " tbody";
    var myTableRows = myTableBody + " tr";
    var myTableColumn = myTable + " th";

    function initTable() {

        $(myTableBody).attr("data-pageSize", 20);
        $(myTableBody).attr("data-firstRecord", 0);
        $('#previous').hide();
        $('#next').show();

        $(myTableColumn).each(function () {
            var width = $(this).width();
            $(this).width(width + 40);
        });

        // Start the pagination
        paginate(parseInt($(myTableBody).attr("data-firstRecord"), 10),
            parseInt($(myTableBody).attr("data-pageSize"), 10));
        $(myTableColumn).eq(0).addClass("sorted-asc");
        //sortTable($(myTable), 0, "asc");
    }

    // Table sorting function
    function sortTable(table, column, order) {
        var asc = order === 'asc';
        var tbody = table.find('tbody');

        tbody.find('tr').sort(function (a, b) {
            if (asc) {
                return $('td:eq(' + column + ')', a).text()
                    .localeCompare($('td:eq(' + column + ')', b).text());
            } else {
                return $('td:eq(' + column + ')', b).text()
                    .localeCompare($('td:eq(' + column + ')', a).text());
            }
        }).appendTo(tbody);
    }

    // Heading click
    $(myTableColumn).click(function () {

        // Start the pagination
        paginate(parseInt($(myTableBody).attr("data-firstRecord"), 10),
            parseInt($(myTableBody).attr("data-pageSize"), 10));
    });

    // Pager click
    $("a.paginate").click(function (e) {
        e.preventDefault();
        var tableRows = $(myTableRows);
        var tmpRec = parseInt($(myTableBody).attr("data-firstRecord"), 10);
        var pageSize = parseInt($(myTableBody).attr("data-pageSize"), 10);

        // Define the new first record
        if ($(this).attr("id") == "next") {
            tmpRec += pageSize;
        } else {
            tmpRec -= pageSize;
        }
        // The first record is < of 0 or > of total rows
        if (tmpRec < 0 || tmpRec > tableRows.length) return

        $(myTableBody).attr("data-firstRecord", tmpRec);
        paginate(tmpRec, pageSize);
    });

    // Paging function
    var paginate = function (start, size) {
        var tableRows = $(myTableRows);
        var end = start + size;
        // Hide all the rows
        tableRows.hide();
        // Show a reduced set of rows using a range of indices.
        tableRows.slice(start, end).show();
        // Show the pager
        $(".paginate").show();
        // If the first row is visible hide prev
        if (tableRows.eq(0).is(":visible")) $('#previous').hide();
        // If the last row is visible hide next
        if (tableRows.eq(tableRows.length - 1).is(":visible")) $('#next').hide();
    }

    // Table starting state
    initTable();

});