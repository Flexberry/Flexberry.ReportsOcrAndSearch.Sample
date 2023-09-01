import Controller from '@ember/controller';
import $ from 'jquery';
import config from '../config/environment';
import { inject as service } from '@ember/service';
import { isEmpty } from '@ember/utils';
import { set } from '@ember/object';

export default Controller.extend({
    appState: service(),

    /**
        Текст для поиска.

        @property searchText
        @type String
        @default undefined
    */
    searchText: undefined,

    /**
        Результаты поиска.
        Массив, который содержит свойства: index, FileName, UploadKey, UploadUrl, PageNumber, TotalPages.

        @property searchResults
        @type Array
        @default null
    */
    searchResults: null,

    /**
        Флаг отображения результатов.

        @property showResults
        @type Boolean
        @default false
    */
    showResults: false,

    /**
        Информация текущего файла.

        @property currentDocumentInfo
        @type Object
        @default null
    */
    currentDocumentInfo: null,

    actions: {
        runSearch(searchText) {
            let appState = this.get('appState');
            let self = this;

            if (!isEmpty(searchText)) {
                appState.loading();
                set(this, 'showResults', false);
                set(this, 'searchResults', null);

                $.ajax({
                    async: true,
                    cache: false,
                    type: 'GET',
                    url: `${config.APP.backendUrls.search}/SearchDocuments?searchText=${searchText}`,
                    dataType: 'json',
                    success(response) {
                        if (response != null && response.length > 0) {
                            var results = response;

                            // Добавим результатам индекс для отображения нумерации.
                            for (var i = 0; i < results.length; i++) {
                                set(results[i], "index", i + 1);
                            }

                            set(self, 'searchResults', results);
                            set(self, 'showResults', true);
                        }
                    },
                    complete() {
                        appState.reset();
                    }
                });
            }
        },

        showModal(currentDocumentInfo) {
            set(this, 'currentDocumentInfo', currentDocumentInfo);

            this.send('showModalDialog', 
                'modal/pdf-document', 
                { controller: 'main-search-form' });
        },
      
        closeModalDialog() {
            this.send('removeModalDialog');
        },
    }
});