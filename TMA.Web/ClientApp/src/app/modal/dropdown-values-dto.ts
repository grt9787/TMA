export class DropdownValuesDto {
  key: string;
  value: string;
  selected: boolean;
  data1?: string = '';
  parentId?: string = '';
  constructor(key: string, value: string, selected = false, data1: string = '', parentId: string = '') {
    this.key = key;
    this.value = value;
    this.selected = selected;
    this.data1 = data1;
    this.parentId = parentId;
  }
}
