import EmberRouter from '@ember/routing/router';
import config from './config/environment';

const Router = EmberRouter.extend({
  location: config.locationType
});

Router.map(function () {
  this.route('i-i-s-reports-ocr-and-search-report-l');
  this.route('i-i-s-reports-ocr-and-search-report-e',
  { path: 'i-i-s-reports-ocr-and-search-report-e/:id' });
  this.route('i-i-s-reports-ocr-and-search-report-e.new',
  { path: 'i-i-s-reports-ocr-and-search-report-e/new' });
});

export default Router;
