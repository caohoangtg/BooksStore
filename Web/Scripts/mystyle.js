$(document).ready(function () {
    //var idleInterval = setInterval("reloadPage()", 20000);
    String.prototype.repeat = function (num) {
        return new Array(num + 1).join(this);
    }
    var filter = ['ass', 'fuck', 'dm', 'sml', 'cc', 'cmn'];
    $('span.timeago').timeago();
    CheckLike();
    $(".anchorLogin").click(function () {
        var $buttonClicked = $(this);
        var id = $buttonClicked.attr('data-id');
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '@Url.Action("_PartialLogin", "Account")',
            contentType: "application/json; charset=utf-8",
            data: { "Id": id },
            datatype: "json",
            success: function (data) {
                $('#myModalContent').html(data);
                $('#login-modal').modal(options);
                $('#login-modal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $(".anchorRegister").click(function () {
        var $buttonClicked = $(this);
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '@Url.Action("_RegisterPartial", "Account")',
            success: function (data) {
                $('#myModalContent').html(data);
                $('#login-modal').modal(options);
                $('#login-modal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $(document).on("click", "#ChangePass", function () {
        //var $buttonClicked = $(this);
        var options = { "backdrop": "static", keyboard: true };
        $.ajax({
            type: "GET",
            url: '@Url.Action("_ChangePasswordPartial", "Account")',
            success: function (data) {
                $('#myModalContent').html(data);
                $('#login-modal').modal(options);
                $('#login-modal').modal('show');
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
    });

    $("#myModalContent").on('click', "#closbtn", function () {
        $('#login-modal').modal('hide');
    });
    //$("#closbtn").click(function () {
    //    console.log("da kcih");
    //    $('#myModal').modal('hide');
    //});
    $(document).on("click", ".submit", function () {
        var data = new FormData;
        data.append("Email", $(".Email").val());
        data.append("Password", $(".Password").val());

        $.ajax({
            type: "POST",
            url: '@Url.Action("_PartialLogin", "Account")',
            //contentType: "application/json; charset=utf-8",
            data: data,
            contentType: false,
            processData: false,
            //datatype: "json",
            success: function (data) {
                //debugger;
                $('#login-modal').modal('hide');
                $('#login').html(data);
            },
            error: function () {
                alert("Dynamic content load failed............");
            }
        });
        return false;
    });
    $(".SinglePost").on("click", ".post", function () {
        var href = $(this).attr("href");
        $.ajax({
            type: "POST",
            url: href,
            success: function (data) {
                $('#main').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
        return false;
    });
    $(document).on("click", "#newTopic", function () {
        var href = $(this).attr("href");
        $.ajax({
            type: "GET",
            url: href,
            success: function (data) {
                $('#main').html(data);
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
        return false;
    });
    $("#search").on("keydown", function (event) {
        if (event.which == 13) {
            var title = $(this).val();
            $.ajax({
                type: "GET",
                url: '@Url.Action("Search", "Home")',
                contentType: "application/json; charset=utf-8",
                data: { "title": title },
                datatype: "json",
                success: function (data) {
                    $("#main").html(data);
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });
        }
    });

    $('#tabs ul li .tab').click(function () {
        var href = $(this).attr("href");
        $.ajax({
            type: "GET",
            url: href,
            success: function (data) {
                $(".showTab").html(data);
            },
            error: function () {
                alert("Something went wrong is controller.........");
            }
        });
        $("#tabs ul li .tab").removeClass("active");
        $(this).addClass("active");
        return false;
    });

    $(".showTab").on("click", "#DelPost", function () {
        var id = $(this).data("id");
        var ps = $(this).parents(".postsingle");
        var r = confirm("Bạn có muốn xóa không!");
        if (r == true) {
            $.ajax({
                type: "POST",
                url: '@Url.Action("DeletePost", "Home")',
                data: { "id": id },
                datatype: "json",
                success: function (data) {
                    ps.remove();
                },
                error: function () {
                    alert("Something went wrong is controller..........");
                }
            });
        }
        return false;
    });

    $(".SinglePost").on("click", ".replys", function () {
        $(this).parents(".media-body").children(":last-child").css("display", "block");
        return false;
    });

    $(".SinglePost").on("click", ".sub-reply", function () {
        var href = $(this).attr("href");
        var reply = $(this).parents(".media-body").find(".comment-reply");
        $(".noreply").remove();
        var data = new FormData;
        data.append("CommentId", $(this).parents(".recent-comment").find("#item_CommentId").val());
        data.append("RepComment", $(this).parents(".reply_form").find("#RepComment").val());
        $(this).parents(".reply_form").find("#RepComment").val("");
        $.ajax({
            type: "POST",
            url: href,
            data: data,
            contentType: false,
            processData: false,
            success: function (data) {
                if (reply.find(".reply").attr("class") == null) {
                    reply.append(data);
                }
                else {
                    reply.find(".reply:last-child").after(data);
                }
            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
        return false;
    });

    $(".comment_form").on("click", ".sub-comment", function () {
        var href = $(this).attr("href");
        var data = new FormData;
        var c = true;
        $(".nocomment").remove();
        data.append("PostId", $("#PostId").val());
        //
        var txt = $("#Comment").val();
        for (var i = 0; i < filter.length; i++) {
            var pattern = new RegExp('\\b' + filter[i] + '\\b', 'g');
            var replacement = '*'.repeat(filter[i].length);
            txt = txt.replace(pattern, replacement);
        }
        //
        if ($("#Comment").val() != txt)
            c = confirm("Bình luận không phù hợp:" + txt);
        if (c == true) {
            data.append("Comment", txt);
            $.ajax({
                type: "POST",
                url: href,
                data: data,
                contentType: false,
                processData: false,
                success: function (data) {
                    $('#comments').before(data);
                    $("#Comment").val("");
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });
        }

        return false;
    });
    $(document).on("click", "#Like", function () {
        var data = new FormData;
        var check = $("i", this);
        data.append("PostId", $("#PostId").val());
        $.ajax({
            type: "POST",
            url: '@Url.Action("Marks", "Home")',
            data: data,
            contentType: false,
            processData: false,
            success: function () {
                if (check.hasClass("text-danger")) {
                    check.removeClass("text-danger");
                    var n = $("#changemark").text();
                    $("#changemark").text(parseInt(n) - 1);
                    //alert("Đã xóa khỏi danh sách đánh dấu");
                }
                else {
                    //alert("Đã thêm vào danh sách đánh dấu");
                    var n = $("#changemark").text();
                    $("#changemark").text(parseInt(n) + 1);
                    check.addClass("text-danger");
                }

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });
        return false;
    });
    $(document).on("click", ".Notifications", function () {
        $(".notify", this).hide();
        $.ajax({
            type: "GET",
            url: '@Url.Action("_NotifyPartial", "Home")',
            success: function (data) {
                $("#menuNotify").append(data);
            },
            error: function () {
                alert("........Dynamic content load failed.");
            }
        });
    });

    $(document).on("click", "#NoLike", function () {
        alert("Bạn cần đăng nhập!");
    });

    $(document).on("click", "#CreatePost", function () {
        //var data = new FormData;
        //data.append("PostId", $("#PostId").val());
        //data.append("Title", $("#Title").val());
        //data.append("CategoryId", $("#CategoryId").val());
        //data.append("TopicId", $("#TopicId").val());
        //data.append("Description", $("#Description").val());
        //var files = $("#imageBrowsers").get(0).files;
        //if (files.length > 0) {
        //    data.append("ImageFile", files[0]);
        //}
        var title = $("#Title").val();
        var kt = 1;
        if (title.length < 16) {
            alert("Tiều đề bài viết phải lớn hơn 16 ký tự");
            return false;
        }
        $.ajax({
            async: false,
            type: "GET",
            url: '@Url.Action("CheckTitle", "Home")',
            data: { "title": title },
            contentType: false,
            datatype: "json",
            success: function (data) {
                if (data != "") {
                    kt = 0;
                    var cTitle = confirm("Bài viết đã tồn tại. Bạn có muốn xem bài tương tự.");
                    if (cTitle == true) {
                        $("#main .card").remove();
                        $("#main").append(data);
                    }
                }

            },
            error: function () {
                alert("Dynamic content load failed.");
            }
        });

        //alert(kt);
        if (kt == 0)
            return false;
        else
            return true;
    });

    $(document).on("change", "#imageBrowsers", function () {
        var File = this.files;
        //var image = image[0].name;
        if (File && File[0]) {
            var file = $("#imageBrowsers").get(0).files;
            var data = new FormData;
            data.append("ImageFile", file[0]);
            $.ajax({
                type: "POST",
                url: "/Home/Upload",
                data: data,
                contentType: false,
                processData: false,
                success: function () {
                    ReadImage(File[0]);
                    alert("sdfsfdsd");
                },
                error: function () {
                    alert("Dynamic content load failed.");
                }
            });

        }
    });
    $(document).on("click", "#CommentDel", function () {
        confirm("Bạn có muốn xóa bình luận không?");
    });
});


function ReadImage(file) {
    var reader = new FileReader;
    var image = new Image;
    reader.readAsDataURL(file);
    reader.onload = function (_file) {
        image.src = _file.target.result;
        image.onload = function () {
            $('#avatar').attr('src', _file.target.result);

        }
    }
}

function CheckLike() {
    if ($(".mark:first").is("#Like")) {
        var id = $("#PostId").val();
        $.ajax({
            type: "GET",
            url: '@Url.Action("CheckMarks", "Home")',
            contentType: "application/json; charset=utf-8",
            data: { "id": id },
            datatype: "json",
            success: function (data) {
                if (data == "1") {
                    $("#Like i").addClass("text-danger");
                }
            },
            error: function () {
                //alert("Dynamic content load failed.");
            }
        });
    }

}
var notificationHub = $.connection.notificationHub;
$.connection.hub.start().done(function () {
    console.log('Notification hub started');
});

//signalr method for push server message to client
notificationHub.client.notify = function (message) {
    if (message && message.toLowerCase() == "added") {
        $.ajax({
            type: "GET",
            url: '@Url.Action("GetNotificationContacts", "Home")',
            success: function (data) {
                if (data == "1") {
                    $('div.notify').show();
                }
            },
            error: function () {
                //alert("Dynamic content load failed.");
            }
        });

    }
}

function reloadPage() {
    console.log("daaaa");
    location.reload();
    //alert("daload");
}