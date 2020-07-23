/* JS Document */

/******************************

[Table of Contents]

1. Vars and Inits
2. Set Header
3. Init Custom Dropdown
4. Init Page Menu
5. Init Deals Slider
6. Init Tab Lines
7. Init Tabs
8. Init Featured Slider
9. Init Favorites
10. Init ZIndex
11. Init Popular Categories Slider
12. Init Banner 2 Slider
13. Init Arrivals Slider
14. Init Arrivals Slider ZIndex
15. Init Best Sellers Slider
16. Init Trends Slider
17. Init Reviews Slider
18. Init Recently Viewed Slider
19. Init Brands Slider
20. Init Timer


******************************/
(function ($) {
    $(window).bind("load", function () {
        function initFSlider(fs) {
            var featuredSlider = fs;
            featuredSlider.on('init', function () {
                var activeItems = featuredSlider.find('.slick-slide.slick-active');
                for (var x = 0; x < activeItems.length - 1; x++) {
                    var item = $(activeItems[x]);
                    item.find('.border_active').removeClass('active');
                    if (item.hasClass('slick-active')) {
                        item.find('.border_active').addClass('active');
                    }
                }
            }).on(
                {
                    afterChange: function (event, slick, current_slide_index, next_slide_index) {
                        var activeItems = featuredSlider.find('.slick-slide.slick-active');
                        activeItems.find('.border_active').removeClass('active');
                        for (var x = 0; x < activeItems.length - 1; x++) {
                            var item = $(activeItems[x]);
                            item.find('.border_active').removeClass('active');
                            if (item.hasClass('slick-active')) {
                                item.find('.border_active').addClass('active');
                            }
                        }
                    }
                })
                .slick(
                    {
                        rows: 2,
                        slidesToShow: 4,
                        slidesToScroll: 4,
                        infinite: false,
                        arrows: false,
                        dots: true,
                        responsive:
                            [
                                {
                                    breakpoint: 768, settings:
                                    {
                                        rows: 2,
                                        slidesToShow: 3,
                                        slidesToScroll: 3,
                                        dots: true
                                    }
                                },
                                {
                                    breakpoint: 575, settings:
                                    {
                                        rows: 2,
                                        slidesToShow: 2,
                                        slidesToScroll: 2,
                                        dots: false
                                    }
                                },
                                {
                                    breakpoint: 480, settings:
                                    {
                                        rows: 1,
                                        slidesToShow: 1,
                                        slidesToScroll: 1,
                                        dots: false
                                    }
                                }
                            ]
                    });
        }


        if ($('.popular_categories_slider').length) {
            var popularSlider = $('.popular_categories_slider');

            popularSlider.owlCarousel(
                {
                    loop: true,
                    autoplay: false,
                    nav: false,
                    dots: false,
                    responsive:
                    {
                        0: { items: 1 },
                        575: { items: 2 },
                        640: { items: 3 },
                        768: { items: 4 },
                        991: { items: 5 }
                    }
                });

            if ($('.popular_categories_prev').length) {
                var prev = $('.popular_categories_prev');
                prev.on('click', function () {
                    popularSlider.trigger('prev.owl.carousel');
                });
            }

            if ($('.popular_categories_next').length) {
                var next = $('.popular_categories_next');
                next.on('click', function () {
                    popularSlider.trigger('next.owl.carousel');
                });
            }
        }
        if ($('.banner_2_slider').length) {
            var banner2Slider = $('.banner_2_slider');
            banner2Slider.owlCarousel(
                {
                    items: 1,
                    loop: true,
                    nav: false,
                    dots: true,
                    dotsContainer: '.banner_2_dots',
                    smartSpeed: 1200
                });
        }
        if ($('.deals_timer_box').length) {
            var timers = $('.deals_timer_box');
            timers.each(function () {
                var timer = $(this);

                var targetTime;
                var target_date;

                // Add a date to data-target-time of the .deals_timer_box
                // Format: "Feb 17, 2018"
                if (timer.data('target-time') !== "") {
                    targetTime = timer.data('target-time');
                    target_date = new Date(targetTime).getTime();
                }
                else {
                    var date = new Date();
                    date.setDate(date.getDate() + 2);
                    target_date = date.getTime();
                }

                // variables for time units
                var days, hours, minutes, seconds;

                var h = timer.find('.deals_timer_hr');
                var m = timer.find('.deals_timer_min');
                var s = timer.find('.deals_timer_sec');

                setInterval(function () {
                    // find the amount of "seconds" between now and target
                    var current_date = new Date().getTime();
                    var seconds_left = (target_date - current_date) / 1000;
                    // do some time calculations
                    days = parseInt(seconds_left / 86400);
                    seconds_left = seconds_left % 86400;

                    hours = parseInt(seconds_left / 3600);
                    hours = hours + days * 24;
                    seconds_left = seconds_left % 3600;


                    minutes = parseInt(seconds_left / 60);
                    seconds = parseInt(seconds_left % 60);

                    if (hours.toString().length < 2) {
                        hours = "0" + hours;
                    }
                    if (minutes.toString().length < 2) {
                        minutes = "0" + minutes;
                    }
                    if (seconds.toString().length < 2) {
                        seconds = "0" + seconds;
                    }

                    // display results
                    h.text(hours);
                    m.text(minutes);
                    s.text(seconds);

                }, 1000);
            });
        }

        if ($('.tabs').length) {
            var tabs = $('.tabs');

            tabs.each(function () {
                var tabsItem = $(this);
                var tabsLine = tabsItem.find('.tabs_line span');
                var tabGroup = tabsItem.find('ul li');

                var posX = $(tabGroup[0]).position().left;
                tabsLine.css({ 'left': posX, 'width': $(tabGroup[0]).width() });
                tabGroup.each(function () {
                    var tab = $(this);
                    tab.on('click', function () {
                        if (!tab.hasClass('active')) {
                            tabGroup.removeClass('active');
                            tab.toggleClass('active');
                            var tabXPos = tab.position().left;
                            var tabWidth = tab.width();
                            tabsLine.css({ 'left': tabXPos, 'width': tabWidth });
                        }
                    });
                });
            });
        }

        if ($('.tabbed_container').length) {
            //Handle tabs switching

            var tabsContainers = $('.tabbed_container');
            tabsContainers.each(function () {
                var tabContainer = $(this);
                var tabs = tabContainer.find('.tabs ul li');
                var panels = tabContainer.find('.panel');
                var sliders = panels.find('.slider');

                tabs.each(function () {
                    var tab = $(this);
                    tab.on('click', function () {
                        panels.removeClass('active');
                        var tabIndex = tabs.index(this);
                        $($(panels[tabIndex]).addClass('active'));
                        sliders.slick("unslick");
                        sliders.each(function () {
                            var slider = $(this);
                            // slider.slick("unslick");
                            if (slider.hasClass('bestsellers_slider')) {
                                initBSSlider(slider);
                            }
                            if (slider.hasClass('featured_slider')) {
                                initFSlider(slider);
                            }
                            if (slider.hasClass('arrivals_slider')) {
                                initASlider(slider);
                            }
                        });
                    });
                });
            });
        }


        if ($('.featured_slider').length) {
            var featuredSliders = $('.featured_slider');
            featuredSliders.each(function () {
                var featuredSlider = $(this);
                initFSlider(featuredSlider);
            });

        }

        if ($('.trends_slider').length) {
            var trendsSlider = $('.trends_slider');
            trendsSlider.owlCarousel(
                {
                    loop: false,
                    margin: 30,
                    nav: false,
                    dots: false,
                    autoplayHoverPause: true,
                    autoplay: false,
                    responsive:
                    {
                        0: { items: 1 },
                        575: { items: 2 },
                        991: { items: 3 }
                    }
                });

            trendsSlider.on('click', '.trends_fav', function (ev) {
                $(ev.target).toggleClass('active');
            });

            if ($('.trends_prev').length) {
                var prev = $('.trends_prev');
                prev.on('click', function () {
                    trendsSlider.trigger('prev.owl.carousel');
                });
            }

            if ($('.trends_next').length) {
                var next = $('.trends_next');
                next.on('click', function () {
                    trendsSlider.trigger('next.owl.carousel');
                });
            }
        }
        if ($('.brands_slider').length) {
            var brandsSlider = $('.brands_slider');

            brandsSlider.owlCarousel(
                {
                    loop: true,
                    autoplay: true,
                    autoplayTimeout: 5000,
                    nav: false,
                    dots: false,
                    autoWidth: true,
                    items: 8,
                    margin: 42
                });

            if ($('.brands_prev').length) {
                var prev = $('.brands_prev');
                prev.on('click', function () {
                    brandsSlider.trigger('prev.owl.carousel');
                });
            }

            if ($('.brands_next').length) {
                var next = $('.brands_next');
                next.on('click', function () {
                    brandsSlider.trigger('next.owl.carousel');
                });
            }
        }

    });


})(jQuery);



