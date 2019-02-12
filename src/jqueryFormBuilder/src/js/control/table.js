import control from '../control';

/**
 * Table - show array data in tabular form
 * Output a <table ... /> html element
 */
export default class controlTable extends control {
  /**
   * definition
   * @return {Object} select control definition
   */
  static get definition() {
    return {
      icon: '▦',
      i18n: {
        default: 'Table'
      }
    };
  }

  /**
  * build a hidden input dom element
  * @return {Object} DOM Element to be injected into the form.
  */
  build() {
    this.dom = this.markup('div', null, { className: 'table table-striped table-sm' })
    return this.dom;
  }

   /**
   * onRender callback //TODO: fix
   */
  onRender() {
    // alert(JSON.stringify(this.config.userData));
    // Set userData if available
    const ok = true;
    if(ok) { // this.config.userData
      let data = [{name:'arghya',id:'101'},{name:'anupam',id:99}]; // this.config.userData[0];
      alert(JSON.stringify(data)); // TODO: check
      if (typeof data == 'string')
        data = JSON.parse(data);
      let tableHtml = '';
      if (data != null && Array.isArray(data)) {
        tableHtml += '<table class="table">';
        for (let row of data) {
            let rowHtml = '<tr>';
            for (let column in row) {
                if (row.hasOwnProperty(column)) {
                    rowHtml += '<td>' + row[column] + '</td>';
                }                
            }
            rowHtml += '</tr>';
            tableHtml += rowHtml;
        }
        tableHtml += '</table>';
      }
      // $('#'+this.config.name).html(tableHtml); // TODO: check
      $('.table').html(tableHtml);
    }
  }
}

// register the following controls
control.register('table', controlTable);