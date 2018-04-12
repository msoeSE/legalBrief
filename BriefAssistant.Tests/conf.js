exports.config = {
    //seleniumAddress: 'http://localhost:4444/wd/hub',
    specs: ['registration-validation-spec.js'],
    framework: 'jasmine2',

    capabilities: {
        'browserName': 'chrome',
        //'maxInstances': 2, // will split your test files across 2 browser instances
        //'shardTestFiles': true,
        chromeOptions: {
            args: ['--headless', '--disable-gpu', '--window-size=800,600', '--no-sandbox']
    }
    }, 

    baseUrl: 'www.briefassistant.com',

    jasmineNodeOpts: {
        showColors: true,
        defaultTimeoutInterval: 30000
    } 
};