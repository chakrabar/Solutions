import control from '../control';

/**
 * Unsable - Error div class
 * Output a <div clsss="error" ... /> form element
 */
export default class controlUnsable extends control {
  /**
   * definition
   * @return {Object} select control definition
   */
  static get definition() {
    return {
      icon: '⌧',
      i18n: {
        default: 'Unusable'
      }
    };
  }

  /**
  * build a hidden input dom element
  * @return {Object} DOM Element to be injected into the form.
  */
  build() {
    this.dom = this.markup('div', '<span class="error">This property cannot be rendered</span>', { className: 'alert alert-danger', role: 'alert' })
    return this.dom;
  }

   /**
   * onRender callback //TODO: fix
   */
  onRender() {
    // Set userData if available
    if(this.config.userData){       
      $('#'+this.config.name).val('This property cannot be rendered. Type: ' + this.config.userData[0]); // TODO: doesn't work
    }
  }
}

// register the following controls
control.register('unusable', controlUnsable);