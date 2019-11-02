if($('.home-slider').length){
	var galleryTop = new Swiper('.home-slider-top', {
		spaceBetween: 0,
		loop: true,
		simulateTouch: false,
		autoplayDisableOnInteraction: false,
	});

	var galleryThumbs = new Swiper('.home-slider-thumbs', {
		spaceBetween: 0,
		centeredSlides: true,
		loop: true,
		direction: 'vertical',
		simulateTouch: false,
		touchRatio: 0.2,
		slideToClickedSlide: true,
		speed: 1500,
		autoplay: {
			delay: 3000,
		},
		breakpoints: {
			1023: {
				direction: 'horizontal',
				centeredSlides: false,
			}
		},
		pagination: {
			el: '.home-slider-thumbs .swiper-pagination',
			clickable: true,
			renderBullet: function (index, className) {
				return '<span class="' + className + '">' + (index + 1) + '</span>';
			},
		},
	});

	galleryTop.controller.control = galleryThumbs;
	galleryThumbs.controller.control = galleryTop;
}

$(document).on("click",".dropdown__active",function(e){
	e.preventDefault()
	$(this).closest(".dropdown").hasClass("active") ? $(".dropdown").removeClass("active") : $(this).closest(".dropdown").addClass("active")
})
$("body").on("click",function(e){
	$(".dropdown").is(e.target)||0!==$(".dropdown").has(e.target).length||0!==$(".active").has(e.target).length||$(".dropdown.active .dropdown-content").closest(".dropdown").removeClass("active")
})

$('.mobile-menu-btn').click(function(){
	if($('body').hasClass('mobileMenuActive')){
		$('body').removeClass('mobileMenuActive');
		$('body').css('overflow', 'auto');
	}else{
		$('body').addClass('mobileMenuActive');
		$('body').css('overflow', 'hidden');
	}
	var body = $("html, body");
	body.stop().animate({scrollTop:0}, 500, 'swing', function() {
	});
})

$(function() {
	$('.tab').each(function(){
		var tab = $(this);
		var tabContent = tab.find('.tab-content');
		var tabMenuLi = tab.find('.tab-menu li');
		var tabMenuLiA = tab.find('.tab-menu li a');
		tabContent.hide();
		tabContent.hide();
		tabContent.first().show();
		tabMenuLi.first().find('a').addClass('active');

		tabMenuLiA.on("click", function(e) {
			e.preventDefault();

			let id = $(this).attr('href');

			tabContent.hide();
			$(id).show();

			tabMenuLiA.removeClass("active");
			$(this).addClass("active");
		});
	})
});

(function ($) {
	$.fn.youtube = function( options ) {
		return this.each(function() {
			var videoId = $(this).attr("href").replace('youtube.com/watch?v=', '').replace('https://', '').replace('www.','');
			var settings = $.extend({
				videoID: videoId,
				autoPlay: true
			}, options );
			if(settings.autoPlay === true) { settings.autoPlay = 1 } else if(settings.autoPlay === false)  { settings.autoPlay = 0 }
			if(videoId) {
				$(this).on( "click", function() {
					$("body").append('<div class="youtube-popup">'+
						'<div class="youtube-popup__content">'+
						'<div class="youtube-popup__close"><svg class="icon icon-close"><use xlink:href="assets/img/icons.svg#icon-close"></use></svg></div>'+
						'<iframe class="youtube-popup__iframe" src="https://www.youtube.com/embed/'+settings.videoID+'?rel=0&wmode=transparent&autoplay='+settings.autoPlay+'&iv_load_policy=3" allowfullscreen frameborder="0"></iframe>'+
						'</div>'+
						'</div>');
				});
			}
			$(this).on('click', function (event) {
				event.preventDefault();
				$(".youtube-popup-close, .youtube-popup").click(function(){
					$(".youtube-popup").remove();
				});
			});
			$(document).keyup(function(event) {
				if (event.keyCode == 27){
					$(".youtube-popup").remove();
				}
			});
		});
	};
}( jQuery ));

$('#promotion').youtube();

baguetteBox.run('.popup-images', {
	animation: 'fadeIn',
	noScrollbars: true
});

$('.accordion').each(function(){
	var accordion = $(this);
	var accordionTitle = accordion.find('.accordion__title');
	accordionTitle.click(function(){
		if(!accordion.hasClass('active')){
			accordion.addClass('active');
		}else{
			$(".accordion.active").removeClass('active');
		}
		var accordionContent = accordion.find('.accordion__content');
		accordionContent.slideToggle('fast');
		$(".accordion__content").not(accordionContent).slideUp('fast');
	});
})


var swiper = new Swiper('.home-media .swiper-container', {
	slidesPerView: '3',
	spaceBetween: 40,
	breakpoints: {
		1023: {
			slidesPerView: 1
		}
	},
	pagination: {
		el: '.home-media .swiper-pagination',
		clickable: true,
	},
});

$('.section-pre-sale-item').hover(
	function() {
		var itemIcon = $(this).find('.section-pre-sale-item__icon');
		if(itemIcon.length > 0){
			var itemIconSrc = itemIcon.attr('src');
			itemIcon.attr('src', itemIconSrc.replace('.', '-hover.'));
		}
	}, function() {
		var itemIcon = $(this).find('.section-pre-sale-item__icon');
		if(itemIcon.length > 0) {
			var itemIconSrc = itemIcon.attr('src');
			itemIcon.attr('src', itemIconSrc.replace('-hover.', '.'));
		}
	})
