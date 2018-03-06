var FollowingsController = (function (followingsService) {
    var button;

    var init = function (container) {
        $(container).on("click", ".js-toggle-follow", toggleFollowing);
    }

    var toggleFollowing = function (e) {
        button = $(e.target);
        var artistId = button.attr("data-artist-id");

        if (button.hasClass("btn-default")) {
            followingsService.createFollowing(artistId, done, fail);
        } else {
            followingsService.deleteFollowing(artistId, done, fail);
        }
    }

    var fail = function () {
        alert("Something failed!");
    };

    var done = function () {
        var text = button.text() == "Following" ? "Follow ?" : "Following";

        button.toggleClass("btn-default").toggleClass("btn-info").text(text);
    };

    return {
        init: init
    }
})(FollowingsService);

