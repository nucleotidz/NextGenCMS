app.service('UserProfile', ['Profile', function (Profile) {
    var userProfile = Profile.get('Profile');

    this.getUserDisplayName = function () {
        var lastName = userProfile.User.lastName;
        return userProfile.User.firstName + (lastName == "" || lastName == undefined ? "" : " " + userProfile.User.lastName);
    };

    this.getUserName = function () {
        return userProfile.User.userName;
    };

    this.isAdminUser = function () {
        return userProfile.User.capabilities.isAdmin;
    }

    this.getUserSite = function () {
        return userProfile.UserSites.shortName;
    }

    this.isManager = function () {
        return (userProfile.UserSites.siteRole == "SiteManager");
    }
}]);


