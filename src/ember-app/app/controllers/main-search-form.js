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
        @default []
    */
    searchResults: [],

    /**
        Флаг отображения результатов.

        @property showResults
        @type Boolean
        @default false
    */
    showResults: false,

    actions: {
        runSearch() {
            let appState = this.get('appState');
            let self = this;

            if (!isEmpty(this.searchText)) {
                appState.loading();
                set(this, 'showResults', false);
                set(this, 'searchResults', undefined);

                $.ajax({
                    async: true,
                    cache: false,
                    type: 'GET',
                    url: `${config.APP.backendUrls.search}/SearchDocuments?searchText=${self.searchText}`,
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
        }
    }
});