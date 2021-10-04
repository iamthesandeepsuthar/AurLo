import { Component, EventEmitter, Output, Input } from '@angular/core';
import { AlertService } from 'src/app/Shared/Services/alert.service';


export class FileInfo {
    private readonly _file: File;
    private readonly _name: string;
    private _fileBase64!: string;
    private readonly _size: string;
    private readonly _sizeSuffix: string;

    constructor(file: File) {
        const SIZES = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];
        this._size = '0';
        this._sizeSuffix = SIZES[0];

        let reader = new FileReader();
        reader.onload = e => {
            this._fileBase64 = e.target!.result!.toString();
        };
        reader.readAsDataURL(file);

        this._file = file;
        this._name = file.name;

        const DECIMALS = 2;
        const KB = 1024;
        const DECIMAL_MARKER = DECIMALS < 0 ? 0 : DECIMALS;

        const i = Math.floor(Math.log(file.size) / Math.log(KB));

        this._size = (file.size / Math.pow(KB, i)).toFixed(DECIMAL_MARKER);
        this._sizeSuffix = SIZES[i];
    }

    get Name(): string {
        return this._name;
    }

    get FileBase64(): string {
        return this._fileBase64;
    }

    get Size(): string {
        return this._size;
    }

    get SizeSuffix(): string {
        return this._sizeSuffix;
    }
}

@Component({
    selector: 'app-file-selector',
    templateUrl: './file-selector.component.html',
    providers: [AlertService]
})
export class FileSelectorComponent {
    private _files: FileInfo[];
    private _allowFiles:string [];

    @Output() readonly FileSelected: EventEmitter<FileInfo>;
    @Output() readonly FilesChanged: EventEmitter<FileInfo[]>;

    constructor(readonly _alertService: AlertService) {
        this.FileSelected = new EventEmitter();
        this.FilesChanged = new EventEmitter();
        this._files = [];
         this.FileFilter = "image/*,.doc,.docx,.ppt,.pptx,.pdf,.xlx,.xlsx,.txt";
        this._allowFiles = ['.jpeg','.gif','.png','.jpg','.TIFF','.PSD','.EPS','.RAW','.INDD','.AI','.doc','.docx','.ppt','.pptx','.pdf','.txt','.xlx','.xlsx','.BMP','.SVG'];

    }

    @Input() FileFilter: string;

    get Files(): FileInfo[] {
        return this._files;
    }

    RemoveFile(file: FileInfo) {
        let index = this._files.indexOf(file);
        if (index > -1)
            this._files.splice(index, 1);
    }

    HandleFileInput(event: any) {
      let files = event.target.Files;
        if (files.length == 0) {
            this.FileSelected.emit(undefined);
            return;
        }
        for (let index = 0; index < files.length; index++) {
            let file = files.item(index);
            let extIndex = file!.name.lastIndexOf('.');
            let ext  = file!.name.substring(extIndex);
           let  isAllowed =  this._allowFiles.some(x=>x === ext);
           if(!isAllowed)
            {
               this._alertService.Error('Selected  file  format not allowed to upload','File Upload');
                return;
            }
            if (file == null || file.size == 0)
                continue;

            let fileInfo = new FileInfo(file);
            this._files.push(fileInfo);

            this.FileSelected.emit(fileInfo);
        }

        this.FilesChanged.emit(this.Files);
    }
}