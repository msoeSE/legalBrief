import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'final',
    templateUrl: './final.component.html'
})
export class FinalComponent {
	private id: string | null;

	constructor(
		private route: ActivatedRoute
	){}

	ngOnInit() {
		this.id = this.route.snapshot.paramMap.get('id');
	}
}
