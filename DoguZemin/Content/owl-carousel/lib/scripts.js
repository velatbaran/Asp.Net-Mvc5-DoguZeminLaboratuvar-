$(document).ready(function(){
    //$('.owl-carousel').owlCarousel({
    //    loop: true,
    //    margin: 10,
    //    nav: true,
    //    autoWidth: true,
    //    autoplay: true,
    //    autoplayTimeout: 2000,
    //    autoplaySpeed: 1000,
    //    autoplayHoverPause: true,
    //    responsive: {
    //        0: {
    //            items: 1
    //        },
    //        600: {
    //            items: 1
    //        },
    //        1000: {
    //            items: 1
    //        }
    //    }
    //});
    $(document).ready(function () {
        var owl = $('.owl-carousel');
        owl.owlCarousel({
            loop: true,
            autoplay: true,
            items: 1,
            dots: false,
            autoplaySpeed: 1000,
            autoplayTimeout: 5000,
            animateOut: 'fadeOut',
            nav: true
        });

        owl.trigger('refresh.owl.carousel');

        $('.play').on('click', function () {
            owl.trigger('play.owl.autoplay', [5000]);
        });

        $('.stop').on('click', function () {
            owl.trigger('stop.owl.autoplay');
        });


    });
  });