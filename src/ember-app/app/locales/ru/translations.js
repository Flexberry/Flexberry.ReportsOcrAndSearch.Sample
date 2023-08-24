import $ from 'jquery';
import EmberFlexberryTranslations from 'ember-flexberry/locales/ru/translations';

import IISReportsOcrAndSearchReportLForm from './forms/i-i-s-reports-ocr-and-search-report-l';
import IISReportsOcrAndSearchReportEForm from './forms/i-i-s-reports-ocr-and-search-report-e';
import IISReportsOcrAndSearchReportModel from './models/i-i-s-reports-ocr-and-search-report';
import MainSearchForm from './forms/main-search-form';

const translations = {};
$.extend(true, translations, EmberFlexberryTranslations);

$.extend(true, translations, {
  models: {
    'i-i-s-reports-ocr-and-search-report': IISReportsOcrAndSearchReportModel,
  },

  'application-name': 'Reports ocr and search',

  forms: {
    loading: {
      'spinner-caption': 'Данные загружаются, пожалуйста подождите...',
    },
    index: {
      greeting: 'Добро пожаловать на тестовый стенд ember-flexberry!',
    },

    application: {
      header: {
        menu: {
          'sitemap-button': {
            title: 'Меню',
          },
          'user-settings-service-checkbox': {
            caption: 'Использовать сервис сохранения пользовательских настроек',
          },
          'show-menu': {
            caption: 'Показать меню',
          },
          'hide-menu': {
            caption: 'Скрыть меню',
          },
          'language-dropdown': {
            caption: 'Язык приложения',
            placeholder: 'Выберите язык',
          },
        },
        login: {
          caption: 'Вход',
        },
        logout: {
          caption: 'Выход',
        },
      },

      footer: {
        'application-name': 'Reports ocr and search',
        'application-version': {
          caption: 'Версия аддона {{version}}',
          title: 'Это версия аддона ember-flexberry, которая сейчас используется в этом тестовом приложении ' +
          '(версия npm-пакета + хэш коммита). ' +
          'Кликните, чтобы перейти на GitHub.',
        },
      },

      sitemap: {
        'application-name': {
          caption: 'Reports ocr and search',
          title: 'Reports ocr and search',
        },
        'application-version': {
          caption: 'Версия аддона {{version}}',
          title: 'Это версия аддона ember-flexberry, которая сейчас используется в этом тестовом приложении ' +
          '(версия npm-пакета + хэш коммита). ' +
          'Кликните, чтобы перейти на GitHub.',
        },
        index: {
          caption: 'Главная',
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
        'main-search-form': {
          caption: 'Поиск',
          title: 'Поиск',
        },
      },
    },

    'edit-form': {
      'save-success-message-caption': 'Сохранение завершилось успешно',
      'save-success-message': 'Объект сохранен',
      'save-error-message-caption': 'Ошибка сохранения',
      'delete-success-message-caption': 'Удаление завершилось успешно',
      'delete-success-message': 'Объект удален',
      'delete-error-message-caption': 'Ошибка удаления',
    },
    'i-i-s-reports-ocr-and-search-report-l': IISReportsOcrAndSearchReportLForm,
    'i-i-s-reports-ocr-and-search-report-e': IISReportsOcrAndSearchReportEForm,
    'main-search-form': MainSearchForm,
  },

});

export default translations;
