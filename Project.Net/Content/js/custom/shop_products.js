new Vue({
    el: '#shop',
    data() {
        return {
            listCategory: [],
            listSupplier: [],
            listProducts: [],
        }
    },
    async created() {
        await this.getCategory()
        await this.getSuppliers()
        await this.getProducts()
        await this.initPage()
    },
    computed: {},
    methods: {
        async getCategory() {
            await axios.get('/Categories', apiBaseOption).then(response => {
                this.listCategory = response.data
            }).catch((error) => {

            })
        },
        async getSuppliers() {
            await axios.get('/Suppliers', apiBaseOption).then(response => {
                this.listSupplier = response.data
                console.log('listSupplier', this.listSupplier)
            }).catch(err => {
                if (err.response && err.response.data) {
                    this.errors = err.response.data.errors
                }
            })
        },
        async getProducts() {
            console.log(id)
            if (id != "") {
                console.log('id', id)
                await axios.get('/Products/ProductInCate/' + id, apiBaseOption).then(response => {
                    this.listProducts = response.data
                    console.log('Product', this.listProducts)
                }).catch((err) => {
                    this.errors = err.response.data.errors
                    console.log("error", err)
                })
                
            } else if (name != "") {
                console.log('name', name)
                await axios.get('/Products/SearchProduct', { ...apiBaseOption, params: {name : name } }).then(response => {
                    this.listProducts = response.data
                    console.log('Product', this.listProducts)
                }).catch((err) => {
                    this.errors = err.response.data.errors
                    console.log("error", err)
                })
            }
            else {
                await axios.get('/Products', apiBaseOption).then(response => {
                    console.log('no Id, Name')
                    this.listProducts = response.data
                    console.log('Product', this.listProducts)
                }).catch((err) => {
                    this.errors = err.response.data.errors
                    console.log("error", err)
                })
            }
        },
        new(note) {
            var flag = false
            note.forEach(function (x) {
                if (x.Notes.AttrName == "new") {
                    flag = true
                    return
                }
            })
            return flag
        },
        discount(note) {
            flag = false
            note.forEach(function (x) {
                if (x.Notes.AttrName == "sale") {
                    flag = true
                    return
                }
            })
            return flag
        },
        classObject: function (note) {
            return {
                'is_new': this.new(note),
                'discount': this.discount(note)
            }
        },
        initPage: function () {
            "use strict";
            /* 

            1. Vars and Inits
        
            */
           console.log('dayla init page')

            var menuActive = false;
            initPageMenu();
            initBrandsSlider();
            initIsotope();
            initPriceSlider();
            initFavs();



            /* 
        
            2. Set Header
        
            */

            /* 
        
            3. Init Custom Dropdown
        
            */

            function initCustomDropdown() {
                if ($('.custom_dropdown_placeholder').length && $('.custom_list').length) {
                    var placeholder = $('.custom_dropdown_placeholder');
                    var list = $('.custom_list');
                }

                placeholder.on('click', function (ev) {
                    if (list.hasClass('active')) {
                        list.removeClass('active');
                    } else {
                        list.addClass('active');
                    }

                    $(document).one('click', function closeForm(e) {
                        if ($(e.target).hasClass('clc')) {
                            $(document).one('click', closeForm);
                        } else {
                            list.removeClass('active');
                        }
                    });

                });

                $('.custom_list a').on('click', function (ev) {
                    ev.preventDefault();
                    var index = $(this).parent().index();

                    placeholder.text($(this).text()).css('opacity', '1');

                    if (list.hasClass('active')) {
                        list.removeClass('active');
                    } else {
                        list.addClass('active');
                    }
                });


                $('select').on('change', function (e) {
                    placeholder.text(this.value);

                    $(this).animate({
                        width: placeholder.width() + 'px'
                    });
                });
            }

            /* 
        
            4. Init Page Menu
        
            */

            function initPageMenu() {
                if ($('.page_menu').length && $('.page_menu_content').length) {
                    var menu = $('.page_menu');
                    var menuContent = $('.page_menu_content');
                    var menuTrigger = $('.menu_trigger');

                    //Open / close page menu
                    menuTrigger.on('click', function () {
                        if (!menuActive) {
                            openMenu();
                        } else {
                            closeMenu();
                        }
                    });

                    //Handle page menu
                    if ($('.page_menu_item').length) {
                        var items = $('.page_menu_item');
                        items.each(function () {
                            var item = $(this);
                            if (item.hasClass("has-children")) {
                                item.on('click', function (evt) {
                                    evt.preventDefault();
                                    evt.stopPropagation();
                                    var subItem = item.find('> ul');
                                    if (subItem.hasClass('active')) {
                                        subItem.toggleClass('active');
                                        TweenMax.to(subItem, 0.3, {
                                            height: 0
                                        });
                                    } else {
                                        subItem.toggleClass('active');
                                        TweenMax.set(subItem, {
                                            height: "auto"
                                        });
                                        TweenMax.from(subItem, 0.3, {
                                            height: 0
                                        });
                                    }
                                });
                            }
                        });
                    }
                }
            }

            function openMenu() {
                var menu = $('.page_menu');
                var menuContent = $('.page_menu_content');
                TweenMax.set(menuContent, {
                    height: "auto"
                });
                TweenMax.from(menuContent, 0.3, {
                    height: 0
                });
                menuActive = true;
            }

            function closeMenu() {
                var menu = $('.page_menu');
                var menuContent = $('.page_menu_content');
                TweenMax.to(menuContent, 0.3, {
                    height: 0
                });
                menuActive = false;
            }

            /* 
        
            5. Init Recently Viewed Slider
        
            */

            function initViewedSlider() {
                if ($('.viewed_slider').length) {
                    var viewedSlider = $('.viewed_slider');

                    viewedSlider.owlCarousel({
                        loop: true,
                        margin: 30,
                        autoplay: true,
                        autoplayTimeout: 6000,
                        nav: false,
                        dots: false,
                        responsive: {
                            0: {
                                items: 1
                            },
                            575: {
                                items: 2
                            },
                            768: {
                                items: 3
                            },
                            991: {
                                items: 4
                            },
                            1199: {
                                items: 6
                            }
                        }
                    });

                    if ($('.viewed_prev').length) {
                        var prev = $('.viewed_prev');
                        prev.on('click', function () {
                            viewedSlider.trigger('prev.owl.carousel');
                        });
                    }

                    if ($('.viewed_next').length) {
                        var next = $('.viewed_next');
                        next.on('click', function () {
                            viewedSlider.trigger('next.owl.carousel');
                        });
                    }
                }
            }

            /* 
        
            6. Init Brands Slider
        
            */

            function initBrandsSlider() {
                if ($('.brands_slider').length) {
                    var brandsSlider = $('.brands_slider');

                    brandsSlider.owlCarousel({
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
            }

            /* 
        
            7. Init Isotope
        
            */

            function initIsotope() {
                var sortingButtons = $('.shop_sorting_button');

                $('.product_grid').isotope({
                    itemSelector: '.product_item',
                    getSortData: {
                        price: function (itemElement) {
                            var priceEle = $(itemElement).find('.product_price').text().replace('$', '');
                            return parseFloat(priceEle);
                        },
                        name: '.product_name div a'
                    },
                    animationOptions: {
                        duration: 750,
                        easing: 'linear',
                        queue: false
                    }
                });

                // Sort based on the value from the sorting_type dropdown
                sortingButtons.each(function () {
                    $(this).on('click', function () {
                        $('.sorting_text').text($(this).text());
                        var option = $(this).attr('data-isotope-option');
                        option = JSON.parse(option);
                        $('.product_grid').isotope(option);
                    });
                });

            }

            /* 
       
           8. Init Price Slider
       
           */

            function initPriceSlider() {
                if ($("#slider-range").length) {
                    $("#slider-range").slider({
                        range: true,
                        min: 0,
                        max: 3000,
                        values: [0, 580],
                        slide: function (event, ui) {
                            $("#amount").val("$" + ui.values[0] + " - $" + ui.values[1]);
                        }
                    });

                    $("#amount").val("$" + $("#slider-range").slider("values", 0) + " - $" + $("#slider-range").slider("values", 1));
                    $('.ui-slider-handle').on('mouseup', function () {
                        $('.product_grid').isotope({
                            filter: function () {
                                var priceRange = $('#amount').val();
                                var priceMin = parseFloat(priceRange.split('-')[0].replace('$', ''));
                                var priceMax = parseFloat(priceRange.split('-')[1].replace('$', ''));
                                var itemPrice = $(this).find('.product_price').clone().children().remove().end().text().replace('$', '');

                                return (itemPrice > priceMin) && (itemPrice < priceMax);
                            },
                            animationOptions: {
                                duration: 200,
                                easing: 'linear',
                                queue: false
                            }
                        });
                    });
                }
            }

            /* 
        
            9. Init Favorites
        
            */

            function initFavs() {
                // Handle Favorites
                var items = document.getElementsByClassName('product_fav');
                for (var x = 0; x < items.length; x++) {
                    var item = items[x];
                    item.addEventListener('click', function (fn) {
                        fn.target.classList.toggle('active');
                    });
                }
            }
        }
    }
})