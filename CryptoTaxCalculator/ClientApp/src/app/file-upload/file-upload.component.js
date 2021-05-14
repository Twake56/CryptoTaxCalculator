"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/common/http");
var FileUploadComponent = /** @class */ (function () {
    function FileUploadComponent(http) {
        this.http = http;
        this.onUploadFinished = new core_1.EventEmitter();
    }
    // At the drag drop area
    // (drop)="onDropFile($event)"
    FileUploadComponent.prototype.onDropFile = function (event) {
        event.preventDefault();
        this.uploadFile(event.dataTransfer.files);
    };
    // At the drag drop area
    // (dragover)="onDragOverFile($event)"
    FileUploadComponent.prototype.onDragOverFile = function (event) {
        event.stopPropagation();
        event.preventDefault();
    };
    // At the file input element
    // (change)="selectFile($event)"
    FileUploadComponent.prototype.selectFile = function (event) {
        this.uploadFile(event.target.files);
    };
    FileUploadComponent.prototype.uploadFile = function (files) {
        var _this = this;
        if (files.length == 0) {
            console.log("No file selected!");
            return;
        }
        var fileToUpload = files[0];
        var formData = new FormData();
        formData.append('file', fileToUpload, fileToUpload.name);
        this.http.post('api/UploadFile/Upload', formData, { reportProgress: true, observe: 'events' })
            .subscribe(function (event) {
            if (event.type === http_1.HttpEventType.UploadProgress)
                _this.progress = Math.round(100 * event.loaded / event.total);
            else if (event.type === http_1.HttpEventType.Response) {
                _this.message = 'Upload success.';
                //this.onUploadFinished.emit(event.body);
            }
        });
        /*this.fileUploadService.uploadFile("/api/UploadFile/Upload", file)
          .subscribe(
            event => {
              if (event.type == HttpEventType.UploadProgress) {
                const percentDone = Math.round(100 * event.loaded / event.total);
                console.log(`File is ${percentDone}% loaded.`);
              } else if (event instanceof HttpResponse) {
                console.log('File is completely loaded!');
              }
            },
            (err) => {
              console.log("Upload Error:", err);
            }, () => {
              console.log("Upload done");
            }
          )*/
    };
    __decorate([
        core_1.Output()
    ], FileUploadComponent.prototype, "onUploadFinished", void 0);
    FileUploadComponent = __decorate([
        core_1.Component({
            selector: 'app-file-upload',
            templateUrl: './file-upload.component.html'
        })
    ], FileUploadComponent);
    return FileUploadComponent;
}());
exports.FileUploadComponent = FileUploadComponent;
//# sourceMappingURL=file-upload.component.js.map