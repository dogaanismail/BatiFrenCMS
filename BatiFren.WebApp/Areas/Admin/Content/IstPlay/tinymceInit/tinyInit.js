//These codes are used for initializing tinymce
tinymce.init({
    selector: '#tinymce', theme: "modern",
    plugins: [
        "advlist autolink autoresize link image fullscreen  lists charmap paste print preview hr anchor pagebreak",
        "searchreplace wordcount visualblocks visualchars insertdatetime media nonbreaking code codesample importcss ",
        "table contextmenu directionality emoticons paste textcolor template responsivefilemanager imagetools youtube bootstrap"
    ],
    preformatted: true,
    menubar: "file edit view insert tools table format",
    toolbar1: "undo redo | bold italic underline | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | styleselect | fullscreen fullpage| template ",
    toolbar2: "|responsivefilemanager| link unlink anchor | image media paste | forecolor backcolor  | print preview code codesample responsivefilemanager youtube ",
    toolbar3: "bootstrap ",


    bootstrapConfig: {
        'allowSnippetManagement': true,
    },
    link_context_toolbar: true,
    image_advtab: true,
    paste_data_images: true,

    external_filemanager_path: "/filemanager/",
    external_plugins: { "filemanager": "/filemanager/plugin.min.js" },
    filemanager_title: "Responsive File Manager"
});