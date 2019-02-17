$(document).ready(function () {
    var resizeTimer;
    var $media = $("#creator-media");
    var $mediaOrientation = $("#creator-media-orientation");

    (function Init() {
        InitMedia();
    })();

    $(window).on('resize', function (e) {
        HandleResize();
    });

    function InitMedia() {
        $media.data("base", $media.css("z-index"));
        $mediaOrientation.data("base", $media.css("z-index"));

        HandleResize(true);
    };   

    function HandleResize(forceTrigger = false) {
        clearTimeout(resizeTimer);

        resizeTimer = setTimeout(function () {
            let runTrigger = false;

            if ($media.data("base") !== $media.css("z-index")) {
                $media.data("base", $media.css("z-index"));

                runTrigger = true;
            }

            if ($mediaOrientation.data("base") !== $mediaOrientation.css("z-index")) {
                $mediaOrientation.data("base", $mediaOrientation.css("z-index"));

                runTrigger = true;
            }

            if (runTrigger || forceTrigger) {
                let sizeInt = $media.css("z-index");
                let size = "xs";
                let orientation = "landscape";

                switch (parseInt($media.css("z-index"))) {
                    case 1:
                        size = "sm";
                        break;

                    case 2:
                        size = "md";
                        break;

                    case 3:
                        size = "lg";
                        break;

                    case 4:
                        size = "xl";
                        break;
                }

                switch ($mediaOrientation.css("z-index")) {
                    case 1:
                        orientation = "portrait";
                        break;

                    case 2:
                        orientation = "landscape";
                        break;
                }

                HandleRows(size);

                $media.trigger("resize", [sizeInt, size, orientation]);
            }
        }, 250);
    }

    function HandleRows(size) { }
});