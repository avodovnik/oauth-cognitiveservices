// Write your JavaScript code.
function uploadBlob(url, blob) {
    var reader = new FileReader();
    // this function is triggered once a call to readAsDataURL returns
    reader.onload = function (event) {
        var fd = new FormData();
        fd.append('FileName', 'test.txt');
        fd.append('Recording', event.target.result);
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
    };
    // trigger the read from the reader...
    reader.readAsDataURL(blob);

}

function do_post_request(url, data, optional_headers = null) {

    console.log(data.size);
    console.log(data.type);

    var objUrl = URL.createObjectURL(data);

    uploadBlob(url, data);

    //var fd = new FormData();
    //fd.append('data', data);
    //$.ajax({
    //    type: 'POST',
    //    url: '/home/test',
    //    data: fd,
    //    processData: false,
    //    contentType: false
    //}).done(function (data) {
    //    console.log(data);
    //    });

    //$.post(url, "blablabla", function () {
    //    console.log(data);
    //    alert("Response received.");
    //}, "json");
}