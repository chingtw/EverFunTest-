$(document).ready(function () {

    /*勾選是否記憶帳號 */
    var userEmail = $.cookie('userEmail');
    if (userEmail) {
        $("#AdminID").val(userEmail);
        $("#invalidCheck").prop("checked",true)
    }
    $("#invalidCheck").change(function () {
        if ($("#invalidCheck").prop('checked')) {
            $.cookie('userEmail', $("#AdminID").val(), { expires: 30 });
        } else {
            $.cookie('userEmail', '');
            $("#AdminID").val("");
        }
    });
    //註冊選項
    $("#Button2").click(function () {
        var bttype = $("#Button2").text();
        if (bttype == "註冊") {
            /*效果*/
            $("#AdminID").hide();
            $("#AdminPW").hide();

            $("#Button1").hide();
            $("#Button2").text("送出註冊");

            $("#AdminID").fadeIn();
            $("#AdminPW").fadeIn();
            $("#AdminName").fadeIn();
            $("#AdminPhome").fadeIn();
            $("#AdminPW2").fadeIn();
        } else {
            PostLogin();
        }
    });
    $('#DisplayDate01').datepicker({
        format: 'yyyy/mm/dd',
    });
    /*資料載入*/
    var pageMode = $("#Mode").val();
    if (pageMode == "info") {
        $("#info").show();
        $("img").attr("src", localStorage.getItem("admin_Photo"));
        $("#infoAdminID").val(localStorage.getItem("admin_id"));
        $("#infoAdminName").val(localStorage.getItem("admin_name"));
        $("#infoAdminEnName").val(localStorage.getItem("admin_EnName"));
        $("#infoAdminPhome").val(localStorage.getItem("admin_phone"));
        $("#infoAdminGender").val(localStorage.getItem("admin_gender"));
        $("#DisplayDate01").val(localStorage.getItem("admin_Birthday"));
        $("#infoAdminAD").val(localStorage.getItem("admin_Address"));
        $("#AddTime").text(localStorage.getItem("AddTime"));
        //localStorage.getItem("admin_Photo", admin_Photo);
    } else {
        $("#Login").show();
    }
});
/* 登入呼叫 */
function LoginByPassword() {
    var email = $("#AdminID").val();
    var pwd = $("#AdminPW").val();
    if (pwd == "" || email == "") {
        callSwalmixin("warning", "請資料輸入完整");
        return;
    }
    if (email != "") {
        /*帳號限EMAIL*/
        var reg = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!reg.test($("#AdminID").val())) {
            callSwalmixin("error", "帳號格式為Email");
            return;
        }
    }
    if (pwd != "") {
        /*須包含最少1個大寫字母、最少1個小寫字母、 最少1個數字，不可有其他符號*/
        var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z0-9]+$/;
        if (!reg.test($("#AdminPW").val())) {
            callSwalmixin("error", "密碼限最少各1個大小寫字母和數字");
            return;
        }
    }
    /*密碼6-30碼字元*/
    if (pwd.length < 6 || pwd.length > 30) {
        callSwalmixin("error", "密碼為6-30碼字元")
        return;
    }
    $.ajax({
        url: '/Home/LoginByPassword/',
        type: 'POST',
        data: {
            email: email,
            password: pwd
        },
        dataType: 'json', beforeSend: function () {
            lodingbx('loding_code');
        },
        success: function (res) {
            if (res.success == true) {
                var rdata = JSON.parse(res.responsedata);
                InfolocalStorage(rdata.admin_id, rdata.admin_pw, rdata.admin_name, rdata.admin_EnName, rdata.admin_phone, rdata.admin_Photo, rdata.admin_gender, rdata.admin_Birthday, rdata.admin_Address, rdata.AddTime);
                $("img").attr("src",rdata.admin_Photo);
                $("#infoAdminID").val(rdata.admin_id);
                $("#infoAdminName").val(rdata.admin_name);
                $("#infoAdminEnName").val(rdata.admin_EnName);
                $("#infoAdminPhome").val(rdata.admin_phone);
                $("#infoAdminGender").val(rdata.admin_gender);
                $("#DisplayDate01").val(rdata.admin_Birthday);
                $("#infoAdminAD").val(rdata.admin_Address);
                $("#AddTime").text(rdata.AddTime);
                Swal.fire({
                    allowOutsideClick: false,
                    icon: 'success',
                    title: "登入成功!",
                    confirmButtonText: '確定',
                    showConfirmButton: false,
                    timer: 1500
                }).then((result) => {
                    $("#Login").hide();
                    $("#info").fadeIn();
                })
            } else {
                callSwal("warning", res.responseText, "帳號或密碼錯誤，或此帳號尚未註冊。")
                return;
            }
        },
        complete: function () {
            closlodingbx()
        },
        error: function (xhr, ajaxOptions, throwError) {
            callSwal("error", "目前系統問題，請稍後再試。", "錯誤代號:" + xhr.status)
            //alert(xhr.status + '|' + ajaxOptions + '|' + throwError);
        }
    });
}
/* 註冊呼叫 */
function PostLogin() {
    var email = $("#AdminID").val();
    var name = $("#AdminName").val();
    var ph = $("#AdminPhome").val();
    var pwd = $("#AdminPW").val();
    var pwd2 = $("#AdminPW2").val();
    if (pwd == "" || email == "" || ph == "" || pwd2 == "" || name == "") {
        callSwalmixin("warning", "請資料輸入完整");
        return;
    }
    if (email != "") {
        /*帳號限EMAIL*/
        var reg = /^([a-zA-Z0-9_\.\-\+])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
        if (!reg.test($("#AdminID").val())) {
            callSwalmixin("error", "帳號格式為Email");
            return;
        }
    }
    
    if (pwd != "") {
        /*須包含最少1個大寫字母、最少1個小寫字母、 最少1個數字，不可有其他符號*/
        var reg = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z0-9]+$/;
        if (!reg.test($("#AdminPW").val())) {
            callSwalmixin("error", "密碼限最少各1個大小寫字母和數字");
            return;
        }
    }
    /*密碼6-30碼字元*/
    if (pwd.length < 6 || pwd.length > 30) {
        callSwalmixin("error", "密碼為6-30碼字元")
        return;
    }
    /*密碼確認*/
    if (pwd != pwd2) {
        callSwalmixin("warning", "密碼確認不符")
        return;
    }
    $.ajax({
        url: '/Home/PostLogin/',
        type: 'POST',
        data: {
            email: email,
            password: pwd,
            name: name,
            phone: ph
        },
        dataType: 'json', beforeSend: function () {
            lodingbx('loding_code');
        },
        success: function (res) {
            if (res.success == true) {
                Swal.fire({
                    allowOutsideClick: false,
                    icon: 'success',
                    title: "註冊成功!",
                    text: "可立即登入",
                    confirmButtonText: '確定',
                }).then((result) => {
                    $("#Button1").fadeIn();
                    $("#Button2").text("註冊");

                    $("#AdminID").fadeIn();
                    $("#AdminPW").fadeIn();
                    $("#AdminName").hide();
                    $("#AdminPhome").hide();
                    $("#AdminPW2").hide();
                })
            } else {
                callSwal("warning", res.responseText, "此帳號或手機已註冊。")
                return;
            }
        },
        complete: function () {
            closlodingbx()
        },
        error: function (xhr, ajaxOptions, throwError) {
            callSwal("error", "目前系統問題，請稍後再試。", "錯誤代號:" + xhr.status)
            //alert(xhr.status + '|' + ajaxOptions + '|' + throwError);
        }
    });
}
/* 登出呼叫 */
function LoginOut() {
    localStorage.clear();
    $.ajax({
        url: '/Home/LoginOut/',
        type: 'POST',
        data: {
        },
        dataType: 'json', beforeSend: function () {
            lodingbx('loding_code');
        },
        success: function (res) {
            if (res.success == true) {
                $("#info").hide();
                $("#Login").fadeIn();
                callSwal("success", res.responseText, "登出成功");
            } else {
                callSwal("warning", res.responseText, "帳號或密碼錯誤，或此帳號尚未註冊。");
                return;
            }
        },
        complete: function () {
            closlodingbx()
        },
        error: function (xhr, ajaxOptions, throwError) {
            callSwal("error", "目前系統問題，請稍後再試。", "錯誤代號:" + xhr.status)
            //alert(xhr.status + '|' + ajaxOptions + '|' + throwError);
        }
    });
}
/* 修改資料 */
function PostInfo() {
    var photo = $('img').attr('src');
    var name = $("#infoAdminName").val();
    var enname =$("#infoAdminEnName").val();
    var phone = $("#infoAdminPhome").val();
    var gender = $("#infoAdminGender").val();
    var birthday = $("#DisplayDate01").val();
    var address = $("#infoAdminAD").val();
    var id = localStorage.getItem("admin_id");
    var pw = localStorage.getItem("admin_pw");
    console.log(photo);
  
       
    $.ajax({
        url: '/Home/GoPostInfo/',
        type: 'POST',
        data: {
           id: id, photoData: photo, name: name, enname: enname, phone: phone, gender: gender, birthday: birthday, address: address, pw: pw
        },
        dataType: 'json', beforeSend: function () {
            lodingbx('loding_code');
        },
        success: function (res) {
            if (res.success == true) {
                var rdata = JSON.parse(res.responsedata);
                InfolocalStorage(rdata.admin_id, rdata.admin_pw, rdata.admin_name, rdata.admin_EnName, rdata.admin_phone, rdata.admin_Photo, rdata.admin_gender, rdata.admin_Birthday, rdata.admin_Address, rdata.AddTime);

                callSwal("success", "更改成功", "");
            } else {
                callSwal("warning", res.responseText, res.info);
                if (res.responseText == "你已自動登出") {
                    LoginOut();
                }
                return;
            }
        },
        complete: function () {
            closlodingbx();
        },
        error: function (xhr, ajaxOptions, throwError) {
            callSwal("error", "目前系統問題，請稍後再試。", "錯誤代號:" + xhr.status)
            //alert(xhr.status + '|' + ajaxOptions + '|' + throwError);
        }
    });
}

function InfolocalStorage(admin_id, admin_pw, admin_name, admin_EnName, admin_phone, admin_Photo, admin_gender, admin_Birthday, admin_Address, AddTime)
{
    localStorage.setItem("admin_id", admin_id);
    localStorage.setItem("admin_pw", admin_pw);
    localStorage.setItem("admin_name", admin_name);
    localStorage.setItem("admin_EnName", admin_EnName);
    localStorage.setItem("admin_phone", admin_phone);
    localStorage.setItem("admin_Photo", admin_Photo);
    localStorage.setItem("admin_gender", admin_gender);
    localStorage.setItem("admin_Birthday", admin_Birthday);
    localStorage.setItem("admin_Address", admin_Address);
    localStorage.setItem("AddTime", AddTime);

}
 /*訊息彈跳視窗*/
function callSwal(type, title, text) {
    Swal.fire({
        allowOutsideClick: false,
        icon: type,
        title: title,
        text: text,
        confirmButtonText: '確定'
    })
}
function callSwalmixin(type, title) {
    const Toast = Swal.mixin({
        toast: true,
        position: 'center',
        showConfirmButton: false,
        timer: 1000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.addEventListener('mouseenter', Swal.stopTimer)
            toast.addEventListener('mouseleave', Swal.resumeTimer)
        }
    })
    Toast.fire({
        icon: type,
        title: title
    })
}
//大頭照上傳
$('#upload_img').on('change', function (e) {
    const file = this.files[0];

    const fr = new FileReader();
    fr.onload = function (e) {
        $('img').attr('src', e.target.result);
    };

    // 轉成 Base64
    fr.readAsDataURL(file);
});

// loding函式
function lodingbx(na) {
    $('#loding_code').removeAttr('style');
    $('body').css('overflow', 'hidden');
    $('#loding_bx,#' + na).fadeIn();
  //  setTimeout(tbox(na), 200);
};
function closlodingbx() {
    $('#loding_bx,#loding_code').fadeOut(100);
    $('body').css('overflow', '');
    setTimeout(function () {
        $('#loding_bx,#loding_code').removeAttr('style');
    }, 200);
};