export class ToolsHelper {
  public static ConvertJsonToFormData(formData: FormData, data: any, parentKey: any = null): void {
    if (data && typeof data === 'object' && !(data instanceof Date) && !(data instanceof File)) {
      Object.keys(data).forEach(key => {
        this.ConvertJsonToFormData(formData, data[key], parentKey ? `${parentKey}[${key}]` : key);
      });
    } else {
      const value = data == null ? '' : data;

      formData.append(parentKey, value);
    }
  }
}
