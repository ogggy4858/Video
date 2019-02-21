var news = {
    Init: function () {
        news.LoadComment();
        news.RegisterEvents();
    },
    RegisterEvents: function () {


        $('#bt').off('click').on('click', function () {
            var text = $('#ContentComment').val();
            news.InsertComment(text);
            news.LoadComment();
        });

        $('.e').off('click').on('click', function () {
            var id = $(this).data('id');
            news.EditComment(id);
        });
        $('.d').off('click').on('click', function () {
            var id = $(this).data('id');
            news.DeleteComment(id);
        });

        $('.like2').off('click').on('click', function () {
            news.LikeNews();
        });
        $('.dislike2').off('click').on('click', function () {
            news.DislikeNews();
        });
    },
    //
    LikeNews: function (ID) {
        $.ajax({
            url: '/Admin/News/LikeNews',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.Status) {
                    news.LoadLD();
                }
            }
        });
    },

    DislikeNews: function (ID) {
        $.ajax({
            url: '/Admin/News/DislikeNews',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.Status) {
                    news.LoadLD();
                }
            }
        });
    },
    //
    LoadLD: function () {
        $.ajax({
            url: '/Admin/News/LoadLD',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                var like = res.Like;
                var dis = res.Dis;
                var li = $('.like2');
                var di = $('.dislike2');

                $('#SpanLike').text(res.CountLike + ' Thích');
                $('#SpanDis').text(res.CountDis + ' Không Thích');
                if (like === false && dis === false) {
                    //
                    li.removeClass('setColor');
                    di.removeClass('setColor');
                }
                if (like === false && dis === true) {
                    li.removeClass('setColor');
                    di.addClass('setColor');
                }
                if (like === true && dis === false) {
                    li.addClass('setColor');
                    di.removeClass('setColor');
                }
            }
        });
    },
    //
    LoadComment: function () {
        news.LoadLD();
        $.ajax({
            url: '/Admin/News/LoadComment',
            type: 'GET',
            dataType: 'json',
            success: function (res) {
                if (res.status) {
                    var data = res.data;
                    var html = '';
                    var temp = $('#template').html();
                    $.each(data, function (i, item) {
                        html = html + Mustache.render(temp, {
                            id: item.ID,
                            date: item.CreateDate,
                            content: item.Content,
                            peopleID: item.PeopleID,
                            email: item.Email,
                            status: item.Status2 === true ? "<span class= \"e\" id=\"edit\" data-id=\"" + item.ID + "\"> Sửa</span>  <span class=\"d\" id=\"delete\" data-id=\"" + item.ID + "\">Xóa</span>" : ""
                        });
                    });
                    $('#divLoadComment').html(html);
                    $('#SpanComment').text(res.CommentNewCount + ' Bình Luận');
                    news.RegisterEvents();
                }
                //else {
                //    alert('false');
                //}
            }
        });
    },
    //
    InsertComment: function (content) {
        $.ajax({
            url: '/Admin/News/InsertComment',
            type: 'POST',
            dataType: 'json',
            data: {
                Content: content
            },
            success: function () {
                news.reset();
            }

        });
    },
    //
    reset: function () {
        $('#ContentComment').val('');
    },
    //
    EditComment: function (ID) {
        $.ajax({
            url: '/Admin/News/GetCommentByID',
            type: 'GET',
            dataType: 'json',
            data: {
                CommentID: ID
            },
            success: function (res) {
                var prom = prompt("Edit Comment", res.text);

                if (prom !== null && prom !== res.text && prom !== '') {

                    var ob = {
                        Content: prom,
                        ID: ID
                    };
                    var text = res.text;
                    $.ajax({
                        url: '/Admin/News/EditComment',
                        type: 'POST',
                        dataType: 'json',
                        data: {
                            model: JSON.stringify(ob)
                        },
                        success: function (res2) {
                            if (res2.Status) {
                                news.LoadComment();
                            }
                        }
                    });

                }
            }

        });
    },

    //
    DeleteComment: function (ID) {
        var con = confirm("Are You Want To Delete This");
        if (con === true) {
            $.ajax({
                url: '/Admin/News/DeleteComment',
                type: 'POST',
                dataType: 'json',
                data: {
                    CommentID: ID
                },
                success: function (res) {
                    news.LoadComment();
                }

            });
        }
    }
};

news.Init();