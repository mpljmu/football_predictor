(function($) {
  "use strict"; // Start of use strict

  // Smooth scrolling using jQuery easing
  $('a.js-scroll-trigger[href*="#"]:not([href="#"])').click(function() {
    if (location.pathname.replace(/^\//, '') == this.pathname.replace(/^\//, '') && location.hostname == this.hostname) {
      var target = $(this.hash);
      target = target.length ? target : $('[name=' + this.hash.slice(1) + ']');
      if (target.length) {
        $('html, body').animate({
          scrollTop: (target.offset().top)
        }, 1000, "easeInOutExpo");
        return false;
      }
    }
  });

  // Closes responsive menu when a scroll trigger link is clicked
  $('.js-scroll-trigger').click(function() {
    $('.navbar-collapse').collapse('hide');
  });

  // Activate scrollspy to add active class to navbar items on scroll
  $('body').scrollspy({
    target: '#sideNav'
  });

})(jQuery); // End of use strict

(function () {
    $(".player-selection").click(function (e) {
        e.preventDefault();
        $("#modalPlayer").modal("show");
    });

    let modalPrediction = $("#modalPrediction");
    $(".future-fixture").click(function (e) {
        e.preventDefault();
        let title = $(this).find(".fixture-title").text();
        let date = $(this).find(".fixture-date").text();
        modalPrediction.find(".fixture-title").text(title);
        modalPrediction.find(".fixture-date").text(date);
        modalPrediction.modal("show");
    });

})();
