(function(window) {
    "use strict";
    var document = window.document,
        $ = window.jQuery;

    var displayThankYouMessage = function () {
        
        $('.feedback').css({ top: $(window).scrollTop() });
        
        setTimeout(function() {
            $('.feedback').css({ top: '-200%' });
            $('body').css({ overflow: 'visible' });
        }, 2000);
        
    };
   
    var sendResponse = function(mood) {
        $.ajax({
            type: "PUT",
            url: "/mood/" + mood,
            contentType: "application/json"
        });

       displayThankYouMessage();
    };
    
    $(window).on('scroll', function(e) {
        $('.scroll-area-smiles').css({ top: -($(window).scrollTop()) });
    });
    
    $(document).ready(function() {

        var actions = {
            'icon-emo-grin': function() {
                sendResponse('happy');
            },
            'icon-emo-sleep': function() {
                sendResponse('whatever');
            },
            'icon-emo-unhappy': function() {
                sendResponse('sad');
            }
        };

        $('.scroll-area-smiles section').on('click', function(e) {
            actions[e.currentTarget.className]();
        });
    });

}(window));