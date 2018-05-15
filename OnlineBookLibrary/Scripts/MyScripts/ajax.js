$(function () {
    var maxPage = $("#maxpage").text();

    $('#maxPageSpan').text(maxPage);

    let pagenum = parseInt($("#currentPage").text());
    if (pagenum <= 1) $("#prev-button").attr("disabled", "disabled");

    $("select").change(
        function () {
            $("#currentPage").text("1");
            $("#books-container").load("Book/TaggedBookPage/" +
                $("#currentPage").text() +
                "/" +
                $(this).val(),
                function () {
                    maxPage = $("#maxpage").text();
                    $('#maxPageSpan').text(maxPage);
                    let pagenum = parseInt($("#currentPage").text());
                    if (pagenum <= 1) $("#prev-button").attr("disabled", "disabled");
                    if (pagenum < maxPage) $("#next-button").removeAttr("disabled");
                    if (pagenum >= maxPage) $("#next-button").attr("disabled", "disabled");
                    if (pagenum > 1) $("#prev-button").removeAttr("disabled");
                }
            );

        });


    $("#prev-button").click(
        function () {
            let pagenum = parseInt($("#currentPage").text());
            if (pagenum > 1) {
                pagenum--;
                $("#currentPage").text(pagenum);
                $("#books-container").load("Book/TaggedBookPage/" +
                    pagenum +
                    "/" +
                    $("select").val(),
                    function () {
                        if (pagenum <= 1) $("#prev-button").attr("disabled", "disabled");
                        if (pagenum < maxPage) $("#next-button").removeAttr("disabled");
                    }
                );
            }
        });

    $("#next-button").click(
        function () {
            let pagenum = parseInt($("#currentPage").text());

            if (pagenum < maxPage) {
                pagenum++;

                $("#currentPage").text(pagenum);
                $("#books-container").load("Book/TaggedBookPage/" +
                    pagenum +
                    "/" +
                    $("select").val(),
                    function () {
                        if (pagenum >= maxPage) $("#next-button").attr("disabled", "disabled");
                        if (pagenum > 1) $("#prev-button").removeAttr("disabled");
                    }
                );


            }
        });
})