var FileImporterPlugin = {
  FileImporterCaptureClick: function() {
    if (!document.getElementById('FileImporter')) {
      var fileInput = document.createElement('input');
      fileInput.setAttribute('type', 'file');
      fileInput.setAttribute('id', 'FileImporter');
      fileInput.setAttribute('accept', '.vrm')
      fileInput.style.visibility = 'hidden';
      fileInput.onclick = function (event) {
        this.value = null;
      };
      fileInput.onchange = function (event) {
        /* This line calls a FileSelected() on the script attached to GameObject named "Scripts"
         * so you need to make sure the scene has a GameObject named "Scripts" and a script with a FileSelected() attached
         *
         * この行はシーン上の"Scripts"という名前のGameObjectに対して、FileSelected()の呼び出しを行っています
         * なので"Scripts"という名前のGameObjectがシーン上にあるかを確認してください
        */
        SendMessage('Scripts', 'FileSelected', URL.createObjectURL(event.target.files[0]));
      }
      document.body.appendChild(fileInput);
    }

    var OpenFileDialog = function() {
      document.getElementById('FileImporter').click();
      document.getElementById('#canvas').removeEventListener('click', OpenFileDialog);
    };

    document.getElementById('#canvas').addEventListener('click', OpenFileDialog, false);
  }
};
mergeInto(LibraryManager.library, FileImporterPlugin);