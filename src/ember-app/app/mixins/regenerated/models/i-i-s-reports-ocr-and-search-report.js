import Mixin from '@ember/object/mixin';
import $ from 'jquery';
import DS from 'ember-data';
import { validator } from 'ember-cp-validations';
import { attr, belongsTo, hasMany } from 'ember-flexberry-data/utils/attributes';

export let Model = Mixin.create({
  reportFile: DS.attr('file')
});

export let ValidationRules = {
  reportFile: {
    descriptionKey: 'models.i-i-s-reports-ocr-and-search-report.validations.reportFile.__caption__',
    validators: [
      validator('ds-error'),
    ],
  },
};

export let defineProjections = function (modelClass) {
  modelClass.defineProjection('ReportE', 'i-i-s-reports-ocr-and-search-report', {
    reportFile: attr('Report file', { index: 0 })
  });

  modelClass.defineProjection('ReportL', 'i-i-s-reports-ocr-and-search-report', {
    reportFile: attr('Report file', { index: 0 })
  });
};
