describe('registration input fields', function () {

    beforeEach(function () {
        browser.waitForAngularEnabled(false);
        browser.ignoreSynchronization = true;
        browser.get(browser.baseUrl + '/loginRegister');
    });

    it('should display error with no registration email', function () {
        var inputField = element(by.name('registrationEmail'));
        inputField.sendKeys('');
        element(by.name('registrationPassword')).click();
        expect(browser.isElementPresent(by.id('registrationEmail-errors'))).toEqual(true);
    });

    it('should display error with no registration password', function () {
        var inputField = element(by.name('registrationPassword'));
        inputField.sendKeys('');
        element(by.name('registrationEmail')).click();
        expect(browser.isElementPresent(by.id('registrationPassword-errors'))).toEqual(true);
    });

    // TEST FOR SPECIFIC PASSWORD FORMAT
    //
    //it('should display error with invalid registration password', function () {
    //    var inputField = element(by.name('registrationPassword'));
    //    inputField.sendKeys('password');
    //    element(by.name('registrationEmail')).click();
    //    expect(browser.isElementPresent(by.id('registrationPassword-errors'))).toEqual(true);
    //});

    it('should display error with no confirm password', function () {
        var inputField = element(by.name('registrationPasswordCheck'));
        inputField.sendKeys('');
        element(by.name('registrationEmail')).click();
        expect(browser.isElementPresent(by.id('registrationPasswordCheck-errors'))).toEqual(true);
    });

    it('should display error with mismatched confirm password', function () {
        var inputField = element(by.name('registrationPassword'));
        inputField.sendKeys('Password');
        inputField = element(by.name('registrationPasswordCheck'));
        inputField.sendKeys('drowssaP');
        element(by.name('registrationEmail')).click();
        expect(browser.isElementPresent(by.id('registrationPasswordCheck-errors'))).toEqual(true);
    });
});