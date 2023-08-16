import Controller from '@ember/controller';
import { computed } from '@ember/object';

export default Controller.extend({
  sitemap: computed('i18n.locale', function () {
    let i18n = this.get('i18n');

    return {
      nodes: [
        {
          link: 'index',
          icon: 'home',
          caption: i18n.t('forms.application.sitemap.index.caption'),
          title: i18n.t('forms.application.sitemap.index.title'),
          children: null
        }, {
          link: null,
          icon: 'list',
          caption: i18n.t('forms.application.sitemap.reports-ocr-and-search.caption'),
          title: i18n.t('forms.application.sitemap.reports-ocr-and-search.title'),
          children: [{
            link: 'i-i-s-reports-ocr-and-search-report-l',
            caption: i18n.t('forms.application.sitemap.reports-ocr-and-search.i-i-s-reports-ocr-and-search-report-l.caption'),
            title: i18n.t('forms.application.sitemap.reports-ocr-and-search.i-i-s-reports-ocr-and-search-report-l.title'),
            icon: 'folder',
            children: null
          }]
        }
      ]
    };
  }),
})