import control from '../control';

/**
 * Table - show array data in tabular form
 * Output a <table ... /> html element
 */
export default class controlTable extends control { // arghya
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
  * onRender callback - at render time, generate a table out of object-array
  * [{'name':'arghya','age':'33'},{'name':'some1','age':'20','designation':'SDE'}] is rendered as
  * name		age		designation
  * arghya	33		
  * some1		20		SDE
  */
  onRender() {
    if (typeof this.config.value == 'undefined' || this.config.value == null)
      return;
    console.log(this.config.value);
    
    let arr = null; // prepare data
    if (typeof this.config.value == 'string') { // we expect an array, if it's string parse it (assuming a json string)
      let str = (' ' + this.config.value).slice(1); // making a copy
      str = str.replace(/'/g, '"'); // if JSON with single-quote, change to double-quote // TODO: can break strings with single quote
      arr = JSON.parse(str);
    } else {
      arr = this.config.value;
    }

    if (arr != null && Array.isArray(arr) && arr.length > 0) {
      let data = $.extend(true, [], arr); // make a copy of data before using
      data = this._normalizeObjectArray(data);
      const firstLetterUpper = str => str.substring(0, 1).toUpperCase() + str.substring(1);
      let tableHtml = '<table class="table">';
      const firstRow = data[0];
      console.log('first row: ' + firstRow);
      const headers = Object.keys(firstRow)
        .map(col => `<th>${firstLetterUpper(col)}</th>`); // To fix: Headers are based on first row data
      tableHtml += `<thead><tr>${headers.join('')}</tr></thead><tbody>`;
      for (let row of data) {
        let rowHtml = '<tr>';
        for (let column in row) {
          if (row.hasOwnProperty(column)) {
            rowHtml += '<td>' + row[column] + '</td>'; // todo: does not handle complex objects in column
          }                
        }
        rowHtml += '</tr>';
        tableHtml += rowHtml;
      }
      tableHtml += '</tbody></table>';
      const targetDiv = `div.field-${this.config.name} > div.table`; // '#'+this.config.name+' > div.table';
      $(targetDiv).html(tableHtml); // NOTE: this depends on auto-generated class names
    }    
  }

  /**
  * Takes array and turns into normalized 2D array with hader. e.g. 
  * [{'name':'arghya','age':'33'},{'name':'some1','designation':'SDE','age':'20'}] becomes
  * [{'name':'arghya','age':'33','designation':''},{'name':'some1','age':'20','designation':'SDE'}]
  * @param {Array.<Object>} arr - an object array
  * @return {Array.<Object>} normalized 2D array 
  **/
  _normalizeObjectArray(arr) {
    if (typeof arr == 'undefined' || arr == null || !Array.isArray(arr) || arr.length == 0)
      return [];

    const data = $.extend(true, [], arr);

    const uniqueProps = new Set();
    for (const obj of data) {
      for (const prop in obj) {
        if (obj.hasOwnProperty(prop)) {
            uniqueProps.add(prop);
        }                
      }
    }

    const matrix = []; // 2D table where earch row is string[] with object fields
    const headers = Array.from(uniqueProps); // array from set
    
    for (const obj of data) {
      if (obj == null || typeof obj != 'object' || Array.isArray(obj)) {
        console.log('Skipping this object from table: ' + obj == null ? 'null' : JSON.stringify(obj));
        continue;
      }        
      const row = {};
      for (const header of headers) {
        if (obj.hasOwnProperty(header)) {
          if (typeof obj[header] == 'object') { // if a property is Object/Array/Null, show empty OR <<OBJECT/ARRAY>>
            if (obj[header] == null) {
              row[header] = '';
            } else if (Array.isArray(obj[header])) {
              console.log('Cannot show array property - ' + JSON.stringify(obj[header]));
              row[header] = '&lt;&ltARRAY&gt;&gt;';
            } else {
              console.log('Cannot show object property - ' + JSON.stringify(obj[header]));
              row[header] = '&lt;&ltOBJECT&gt;&gt;';
            }            
          } else {
            row[header] = obj[header];
          }
        } else {
          row[header] = '';
        }
      }
      matrix.push(row);
    }

    return matrix;
  }
}

// register the following controls
control.register('table', controlTable); // main table control
control.register('table', controlTable, 'table'); // subtype