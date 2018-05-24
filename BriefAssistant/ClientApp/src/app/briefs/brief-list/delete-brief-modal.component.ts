import { Component, Input } from '@angular/core';

import { NgbActiveModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  template:
    `<div class="modal-header">
    <h4 class="modal-title">Delete Brief</h4>
    <button type="button" class="close" aria-label="Close" (click)="activeModal.dismiss();">
      <span aria-hidden="true">&times;</span>
    </button>
  </div>
  <div class="modal-body">
    <p>Are you sure you want to delete "{{briefName}}" brief?</p>
  </div>
  <div class="modal-footer">
    <button type="button" class="btn btn-outline-secondary" (click)="activeModal.dismiss()">Cancel</button>
    <button type="button" class="btn btn-outline-danger" (click)="activeModal.close()">Delete</button>
  </div>`
})
export class DeleteBriefModalComponent {
  @Input() briefName: string;

  constructor(public activeModal: NgbActiveModal) { }
}
