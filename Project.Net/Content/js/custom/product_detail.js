const cartItem = {
    Quantity: 1,
	ProductId: parseInt(id),

}
new Vue({
            el: '#product_detail',
    data() {
        return {
            listSupplier: [],
            product: '',
			product_id: id,
            model: Object.assign({},cartItem),
                    colorAttr: '',
					romAttr: '',
					price:''
				
				
                }
            },
            async created() {
                await this.getSuppliers()
               await this.fetchData(this.id)
               await this.initPage()
            },
            computed: {
                listProImage() {
                    return (this.product.ImageList || "").split(';');
                },
                rom() {
                    return this.product.ProductAttrs.filter(x => x.Attribute.AttributeTypes.TypeName == "ROM");
                },
                color() {
					return this.product.ProductAttrs.filter(x => x.Attribute.AttributeTypes.TypeName == "Color");
                }
            },
    methods: {
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
        async fetchData() {
            await axios.get('/Products/' + this.product_id, apiBaseOption).then(response => {
                this.product = response.data
                console.log('Product', this.product)
            }).catch((err) => {
                this.errors = err.response.data.errors
                console.log("error", err)
            })
        },
        async AddtoCart() {
            if (this.colorAttr == '' || this.romAttr == '') {
				Command: toastr["error"]("Color or Rom must not be blank")
                toastr.options = {
                    "closeButton": false,
                    "debug": false,
                    "newestOnTop": false,
                    "progressBar": false,
                    "positionClass": "toast-top-right",
                    "preventDuplicates": false,
                    "onclick": null,
                    "showDuration": "300",
                    "hideDuration": "1000",
                    "timeOut": "5000",
                    "extendedTimeOut": "1000",
                    "showEasing": "swing",
                    "hideEasing": "linear",
                    "showMethod": "fadeIn",
                    "hideMethod": "fadeOut"
                }
                return
            }

			this.model = { ...this.model, Attributes: this.colorAttr + ',' + this.romAttr, price: this.price }
			console.log(this.model);
            await axios.post('/Cart/AddToCart', this.model).then(() => {
                window.location.href = "/Cart"
            })
                .catch((err) => {
                    this.errors = err.response.data.errors
                    console.log("error", err)
                })
        },
        initPage: function () {
            "use strict";

            /* 
 
1. Vars and Inits
 
*/
            console.log('day la init page')
            var menuActive = false;

            initPageMenu();
            initViewedSlider();
            initBrandsSlider();
            initQuantity();
            initColor();
			initFavs();
			initImage();

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

                    7. Init Quantity

                    */

                    function initQuantity() {
                        // Handle product quantity input
                        if ($('.product_quantity').length) {
                            var input = $('#quantity_input');
                            var incButton = $('#quantity_inc_button');
                            var decButton = $('#quantity_dec_button');

                            var originalVal;
                            var endVal;

                            incButton.on('click', function () {
                                originalVal = input.val();
                                endVal = parseFloat(originalVal) + 1;
                                input.val(endVal);
                            });

                            decButton.on('click', function () {
                                originalVal = input.val();
                                if (originalVal > 0) {
                                    endVal = parseFloat(originalVal) - 1;
                                    input.val(endVal);
                                }
                            });
                        }
                    }

                    /* 

                    8. Init Color

                    */

                    function initColor() {
                        if ($('.product_color').length) {
                            var selectedCol = $('#selected_color');
                            var colorItems = $('.color_list li .color_mark');
                            colorItems.each(function () {
                                var colorItem = $(this);
                                colorItem.on('click', function () {
                                    var color = colorItem.css('backgroundColor');
                                    selectedCol.css('backgroundColor', color);
                                });
                            });
                        }
                    }

                    /* 

                    9. Init Favorites

                    */

                    function initFavs() {
                        // Handle Favorites
                        var fav = $('.product_fav');
                        fav.on('click', function () {
                            fav.toggleClass('active');
                        });
                    }

                    /* 

                    10. Init Image

                    */

                    function initImage() {
                        var images = $('.image_list li');
                        var selected = $('.image_selected img');

                        images.each(function () {
                            var image = $(this);
                            image.on('click', function () {
                                var imagePath = new String(image.data('image'));
                                selected.attr('src', imagePath);
                            });
                        });
                    }
                }
            }
            })