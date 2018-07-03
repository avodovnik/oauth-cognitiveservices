function do_post_request(url, data, optional_headers = null) {
    console.log(data.size);
    console.log(data.type);

    var objUrl = URL.createObjectURL(data);

    var fd = new FormData();
    fd.append('Recording', data);
    $.ajax({
        type: 'POST',
        url: url,
        data: fd,
        processData: false,
        contentType: false
    }).done(function (data) {
        // print the output from the upload.php script
        console.log(data);
    });
}