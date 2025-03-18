interface IDebounceConfiguration { blazorCallBackRef: any, callBackName: string, regexPattern: string, validateEntry: boolean, delayMs: number, liveRegionID: string, liveText:string};
interface IDebouncedTextResult { TextValue: string, IsValid: boolean, ExceptionMessage:string | null };

const _handlerMap = new WeakMap<HTMLInputElement, { configuration: IDebounceConfiguration, handler: EventListener; timer?: number }>();
function raiseDebounceInterval(inputElement: HTMLInputElement, debounceConfiguration: IDebounceConfiguration) {

    if (!debounceConfiguration) return;

    if (!debounceConfiguration.blazorCallBackRef)  return;

    let isValid = true;
    let message = null;

    if (true === debounceConfiguration.validateEntry && debounceConfiguration.regexPattern) {

        const liveRegionElement = document.getElementById(debounceConfiguration.liveRegionID) as HTMLElement;

        if (liveRegionElement) liveRegionElement.innerText = "";

        try {

            isValid = new RegExp(debounceConfiguration.regexPattern).test(inputElement.value);

            if (liveRegionElement && false == isValid) liveRegionElement.innerText = debounceConfiguration.liveText;
                
        }
        catch (ex:any)
        {
            if (liveRegionElement) liveRegionElement.innerText = "System error, filtering is unavailable at this time.";
            message = ex.message;
            isValid = false;
        }

        if (liveRegionElement !== null && liveRegionElement.innerText.length > 0) setTimeout(() => liveRegionElement.innerText = "", 3000);

        inputElement.setAttribute("aria-invalid", (!isValid).toString().toLowerCase());
    }

    const debouncedText: IDebouncedTextResult = { TextValue: inputElement.value, IsValid: isValid, ExceptionMessage: message }; 

    debounceConfiguration.blazorCallBackRef.invokeMethodAsync(debounceConfiguration.callBackName, debouncedText);
}
function handleOnInput(event:Event): void {

    if (!(event.target instanceof HTMLInputElement)) return;

    const inputElement = event.target;
    const mapEntry     = _handlerMap.get(inputElement);

    if (!mapEntry) return;

    const { configuration } = mapEntry;

    if (mapEntry.timer) clearTimeout(mapEntry.timer);

    mapEntry.timer = setTimeout(raiseDebounceInterval, configuration.delayMs, inputElement, configuration);
  
}
function registerTextDebounce(inputElement: HTMLInputElement, debounceConfiguration: IDebounceConfiguration): void {

    if (!inputElement || _handlerMap.has(inputElement) || !debounceConfiguration) return;

    const handler = (event: Event) => handleOnInput(event);

    unRegisterTextDebounce(inputElement);

    _handlerMap.set(inputElement, {
        configuration: debounceConfiguration,
        handler: handler
    });

    inputElement.addEventListener("input", handler);
}
function unRegisterTextDebounce(inputElement: HTMLInputElement) {

    if (!inputElement) return;

    const mapEntry = _handlerMap.get(inputElement);

    if (mapEntry) {
        inputElement.removeEventListener("input", mapEntry.handler);
        _handlerMap.delete(inputElement);
    }
}

function clearTextValue(inputElement: HTMLInputElement) {

    if (!inputElement) return;
    inputElement.value = "";
    inputElement.removeAttribute("aria-invalid");
}

export { registerTextDebounce, unRegisterTextDebounce, clearTextValue };