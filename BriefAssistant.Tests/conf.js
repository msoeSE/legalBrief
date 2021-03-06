﻿exports.config = {
    specs: ['form-validation-spec.js', 'registration-validation-spec.js', 'reply-form-validation-spec.js'], //'response-form-validation-spec.js'],
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
        browser.get(browser.baseUrl + '/account/login-register');
        element(by.name('loginEmail')).sendKeys(process.env.MY_SECRET_ENV_USER);
        element(by.name('loginPassword')).sendKeys(process.env.MY_SECRET_ENV_PASSWORD);
        browser.sleep(1000);
        element(by.name('loginButton')).click();
        browser.sleep(1000);
    }
};