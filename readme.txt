Buoc 1: Copy file loading.css vao thu muc CSS/Views
Buoc 2: Import file loading.css vao trang html
<link href="./CSS/Views/loading.css" rel="stylesheet" />
Buoc 3: Them dong code sau ngay ben duoi the <body>
<div id="ajax_loader" class="ajax-load-qa"> &nbsp;</div>
Buoc 4: Dung 2 cau lenh sau de kich hoat va huy loading tai cho minh muon
$("#ajax_loader").css("display", "block");// Kích hoat loading
$("#ajax_loader").css("display", "none"); // Huy loading
