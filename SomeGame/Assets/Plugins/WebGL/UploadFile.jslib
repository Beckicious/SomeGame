mergeInto(LibraryManager.library, 
{
    BrowserTextUpload: function(extFilter, gameObjName, dataSinkFn)
    {
        if (typeof window.unityInstance === 'undefined' || window.unityInstance === null) {
            console.error('unityInstance is not defined');
            return;
        }

        var input = document.createElement('input');
        input.type = 'file';
        input.accept = '.csv';

        input.onchange = function(event) {
            var file = event.target.files[0];
            if (file) {
                var reader = new FileReader();
                reader.onload = function(e) {
                    var content = e.target.result;
                    window.unityInstance.SendMessage(gameObjName, dataSinkFn, content);
                };
                reader.readAsText(file);
            }
        };

        input.click();
    }
});