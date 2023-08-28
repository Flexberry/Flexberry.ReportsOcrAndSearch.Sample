import PdfJs from 'ember-pdf-js/components/pdf-js';
 
export default PdfJs.extend({
  /**
    Страница на которую перепрыгнет pdf при открытии.

    @property startPage
    @type Number
    @default 1
  */  
  startPage: 1,

  didInsertElement () {
    this._super(...arguments);
    
    const pdfViewer = this.get('pdfViewer');
    const _this = this;

    pdfViewer.eventBus.on('pagesinit', function() {
      const startPage = _this.get('startPage');
      _this.send('changePage', startPage);
    });
  },
})
