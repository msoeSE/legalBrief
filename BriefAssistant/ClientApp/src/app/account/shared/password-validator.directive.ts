import { Directive, forwardRef, Attribute } from "@angular/core";
import { Validator, AbstractControl, NG_VALIDATORS } from "@angular/forms";

@Directive({
    selector: '[validateEqual][formControlName],[validateEqual][formControl],[validateEqual][ngModel]',
    providers: [
        {
            provide: NG_VALIDATORS,
            useExisting: forwardRef(() => PasswordValidator),
            multi: true
        }
    ]
})
export class PasswordValidator implements Validator {

    constructor( @Attribute('validateEqual') public validateEqual: string,
        @Attribute('reverse') public reverse: string) {}

    private get isReverse() {
        if (!this.reverse) return false;
        return this.reverse === 'true';
    }

    validate(control: AbstractControl): { [key: string]: any } {
        // self value (e.g. retype password)
        let controlValue = control.value;
        // control value (e.g. password)
        let check = control.root.get(this.validateEqual);
        // value not equal
        if (check && controlValue !== check.value && !this.isReverse)
            return {
                validateEqual: false
            };

        // value equal and reverse
        if (check && controlValue === check.value && this.isReverse) {
            delete check.errors['validateEqual'];
            if (!Object.keys(check.errors).length) check.setErrors(null);
        }

        // value not equal and reverse
        if (check && controlValue !== check.value && this.isReverse) {
            check.setErrors({
                validateEqual: false
            });
        }
        return null;
    }
}