describe('dataform input fields', function () {

    beforeEach(function () {
        browser.waitForAngularEnabled(false);
        browser.ignoreSynchronization = true;
        browser.get(browser.baseUrl + '/briefs/initial/new');
        browser.switchTo().alert().accept();
    });

    it('should display error with no name', function () {
        var inputField = element(by.name('name'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('name-errors'))).toEqual(true);
    });

    it('should display error with too short of name', function () {
        var inputField = element(by.name('name'));
        inputField.sendKeys('AAA');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('name-errors'))).toEqual(true);
    });

    it('should display error with no street', function () {
        var inputField = element(by.name('street'));
        inputField.sendKeys('');
        element(by.name('name')).click();
        expect(browser.isElementPresent(by.id('street-errors'))).toEqual(true);
    });

    it('should display error with no city', function () {
        var inputField = element(by.name('city'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('city-errors'))).toEqual(true);
    });

    it('should display error with no zip', function () {
        var inputField = element(by.name('zip'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('zip-errors'))).toEqual(true);
    });

    it('should display error with no phone', function () {
        var inputField = element(by.name('phone'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('phone-errors'))).toEqual(true);
    });

    it('should display error with wrongly formatted phone', function () {
        var inputField = element(by.name('phone'));
        inputField.sendKeys('533jkjjf92');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('phone-errors'))).toEqual(true);
    });

    it('should display error with no email', function () {
        var inputField = element(by.name('email'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('email-errors'))).toEqual(true);
    });

    it('should display error with no circuit court case number', function () {
        var inputField = element(by.name('circuitCourtCaseNumber'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('circuitCourtCaseNumber-errors'))).toEqual(true);
    });

    it('should display error with wrongly formatted circuit court case number', function () {
        var inputField = element(by.name('circuitCourtCaseNumber'));
        inputField.sendKeys('4324afdsaasfd');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('circuitCourtCaseNumber-errors'))).toEqual(true);
    });

    it('should display error with no judge first name', function () {
        var inputField = element(by.name('judgeFirstname'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('judgeFname-errors'))).toEqual(true);
    });

    it('should display error with no judge last name', function () {
        var inputField = element(by.name('judgeLastname'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('judgeLname-errors'))).toEqual(true);
    });

    it('should display error with no opponent name', function () {
        var inputField = element(by.name('opponentName'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('opponentName-errors'))).toEqual(true);
    });

    it('should display error with no appellate court case number', function () {
        var inputField = element(by.name('appellateCourtCaseNumber'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('appellateCourtCaseNumber-errors'))).toEqual(true);
    });

    it('should display error with wrongly formatted appellate court case number', function () {
        var inputField = element(by.name('appellateCourtCaseNumber'));
        inputField.sendKeys('4324afdsaasfd');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('appellateCourtCaseNumber-errors'))).toEqual(true);
    });

    it('should display error with no issues presented', function () {
        var inputField = element(by.name('issuesPresented'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('issuesPresented-errors'))).toEqual(true);
    });

    it('should display error with no oral argument statement', function () {
        var inputField = element(by.name('oralArgumentStatement'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('oralArgumentStatement-errors'))).toEqual(true);
    });

    it('should display error with no publication statement', function () {
        var inputField = element(by.name('publicationStatement'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('publicationStatement-errors'))).toEqual(true);
    });

    it('should display error with no case and facts statement', function () {
        var inputField = element(by.name('caseFactsStatement'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('caseFactsStatement-errors'))).toEqual(true);
    });

    it('should display error with no argument', function () {
        var inputField = element(by.name('argument'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('argument-errors'))).toEqual(true);
    });

    it('should display error with no conclusion', function () {
        var inputField = element(by.name('conclusion'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('conclusion-errors'))).toEqual(true);
    });

    it('should display error with no appendix documents', function () {
        var inputField = element(by.name('appendixDocuments'));
        inputField.sendKeys('');
        element(by.name('street')).click();
        expect(browser.isElementPresent(by.id('appendixDocuments-errors'))).toEqual(true);
    });
});