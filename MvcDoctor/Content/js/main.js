(function ($) {
 "use strict";

/*---------------------
	Slider-active
----------------------*/
$('.slider-active').owlCarousel({
    loop:true,
    nav:true,
	navText:['<i class="fa fa-angle-left"></i> P','<i class="fa fa-angle-right"></i> N'],
    responsive:{
        0:{
            items:1
        },
        600:{
            items:1
        },
        1000:{
            items:1
        }
    }
})

/*---------------------
  Preloader
--------------------- */

    $(window).on('load', function() {
        $('#preloader').fadeOut('slow', function() { $(this).remove(); });
    });
	

/*---------------------
 Mouse Hover Next Slide Visible
----------------------*/ 
    var next_slide = $('.slider-area .owl-nav .owl-next');
    next_slide.on('mouseover', function() {
        $('#slider .owl-item.active').next('.owl-item').addClass('slide-pull-next').removeClass('slide-pull-stay');
    });
    next_slide.on('mouseleave', function() {
        $('#slider .owl-item.active').next('.owl-item').removeClass('slide-pull-next');
    });
    next_slide.on('click', function() {
        $('#slider .owl-item.active').addClass('slide-pull-stay').removeClass('slide-pull-next slide-pull-stay');
    });

/*-------------------
  Scroll-up
--------------------*/
    $.scrollUp({
        scrollText: '<i class="fa fa-angle-double-up " aria-hidden="true"></i>',
        easingType: 'linear',
        scrollSpeed: 900,
        animation: 'fade'
    });
 
/*----------------------------
  Sticky Header
------------------------------ */
$(window).on('scroll',function() {    
	var scroll = $(window).scrollTop();
	if (scroll < 200) {
	$("#main_h").removeClass("sticky");
	}else{
	$("#main_h").addClass("sticky");
	}
}); 

/*----------------------------
  progress-bar
------------------------------ */ 
 $('.about-skill-area').waypoint(function() {
    $('.progress-bar').addClass('left-anim');
}, {
    triggerOnce: true,
    offset: '70%'
});
 
 /*----------------------------
 jQuery MeanMenu
------------------------------ */
	jQuery('#mobile-menu').meanmenu();		
	
})(jQuery);  