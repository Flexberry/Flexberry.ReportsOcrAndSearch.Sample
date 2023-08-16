import $ from 'jquery';
import EmberFlexberryTranslations from 'ember-flexberry/locales/en/translations';

import IISReportsOcrAndSearchReportLForm from './forms/i-i-s-reports-ocr-and-search-report-l';
import IISReportsOcrAndSearchReportEForm from './forms/i-i-s-reports-ocr-and-search-report-e';
import IISReportsOcrAndSearchReportModel from './models/i-i-s-reports-ocr-and-search-report';

const translations = {};
$.extend(true, translations, EmberFlexberryTranslations);

$.extend(true, translations, {
  models: {
    'i-i-s-reports-ocr-and-search-report': IISReportsOcrAndSearchReportModel,
  },

  'application-name': 'Reports ocr and search',

  forms: {
    loading: {
      'spinner-caption': 'Loading stuff, please wait for a moment...',
    },
    index: {
      greeting: 'Welcome to ember-flexberry test stand!',
    },

    application: {
      header: {
        menu: {
          'sitemap-button': {
            title: 'Menu',
          },
          'user-settings-service-checkbox': {
            caption: 'Use service to save user settings',
          },
          'show-menu': {
            caption: 'Show menu',
          },
          'hide-menu': {
            caption: 'Hide menu',
          },
          'language-dropdown': {
            caption: 'Application language',
            placeholder: 'Choose language',
          },
        },
        login: {
          caption: 'Login',
        },
        logout: {
          caption: 'Logout',
        },
      },

      footer: {
        'application-name': 'Reports ocr and search',
        'application-version': {
          caption: 'Addon version {{version}}',
          title: 'It is version of ember-flexberry addon, which uses in this dummy application ' +
          '(npm version + commit sha). ' +
          'Click to open commit on GitHub.',
        },
      },

      sitemap: {
        'application-name': {
          caption: 'Reports ocr and search',
          title: 'Reports ocr and search',
        },
        'application-version': {
          caption: 'Addon version {{version}}',
          title: 'It is version of ember-flexberry addon, which uses in this dummy application ' +
          '(npm version + commit sha). ' +
          'Click to open commit on GitHub.',
        },
        index: {
          caption: 'Home',
          title: '',
        },
        'reports-ocr-and-search': {
          caption: 'ReportsOcrAndSearch',
          title: 'ReportsOcrAndSearch',
          'i-i-s-reports-ocr-and-search-report-l': {
            caption: 'Report',
            title: '',
          },
        },
      },
    },

    'edit-form': {
      'save-success-message-caption': 'Save operation succeed',
      'save-success-message': 'Object saved',
      'save-error-message-caption': 'Save operation failed',
      'delete-success-message-caption': 'Delete operation succeed',
      'delete-success-message': 'Object deleted',
      'delete-error-message-caption': 'Delete operation failed',
    },
    'i-i-s-reports-ocr-and-search-report-l': IISReportsOcrAndSearchReportLForm,
    'i-i-s-reports-ocr-and-search-report-e': IISReportsOcrAndSearchReportEForm,
  },

});

export default translations;
