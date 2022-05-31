$(document).ready(function(){
    function Month(monthNumber) {
        var months = ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'];
        return months[monthNumber - 1];
    }
    function RangeIndex(obj, fd){
        let d = fd.substring(5).split("-");
        let month = d[1] + " " + Month(d[0]);
        $(obj).parent().children("div").text(month);
    }
    $("input[type='date']").on("change", function(){
        let fd = $($(this)).val();
        RangeIndex($(this), fd);
    });
    $("input[type='date']").each(function(){
        var todaydate = $(this).attr("value");
        RangeIndex($(this), todaydate);
    });
    $('.owl-carousel').owlCarousel({
        loop: true,
        margin: 0,
        nav: false,
        items: 1
    });
});