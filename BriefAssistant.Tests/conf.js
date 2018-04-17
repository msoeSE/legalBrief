﻿exports.config = {
    //seleniumAddress: 'http://localhost:4444/wd/hub',
    specs: ['initial-form-validation-spec.js', 'registration-validation-spec.js'],
    framework: 'jasmine2',

    capabilities: {
        'browserName': 'chrome',
        //'maxInstances': 2, // will split your test files across 2 browser instances
        //'shardTestFiles': true,
        chromeOptions: {
            args: ['--headless', '--disable-gpu', '--window-size=800,600', '--no-sandbox']
        }
    }, 

    baseUrl: 'https://briefassistant.com',

    jasmineNodeOpts: {
        showColors: true,
        defaultTimeoutInterval: 30000
    }, 

    onPrepare: function () {
        browser.get(browser.baseUrl + '/loginRegister');
        element(by.name('loginEmail')).sendKeys("THIS IS NOT THE EMAIL");
        element(by.name('loginPassword')).sendKeys("THIS IS NOT THE PASSWORD");
        browser.sleep(1000);
        element(by.name('loginButton')).click();
        browser.sleep(1000);
    }
};