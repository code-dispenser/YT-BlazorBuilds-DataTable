;
;
const _handlerMap = new WeakMap();
function raiseDebounceInterval(inputElement, debounceConfiguration) {
    if (!debounceConfiguration)
        return;
    if (!debounceConfiguration.blazorCallBackRef)
        return;
    let isValid = true;
    let message = null;
    if (true === debounceConfiguration.validateEntry && debounceConfiguration.regexPattern) {
        const liveRegionElement = document.getElementById(debounceConfiguration.liveRegionID);
        if (liveRegionElement)
            liveRegionElement.innerText = "";
        try {
            isValid = new RegExp(debounceConfiguration.regexPattern).test(inputElement.value);
            if (liveRegionElement && false == isValid)
                liveRegionElement.innerText = debounceConfiguration.liveText;
        }
        catch (ex) {
            if (liveRegionElement)
                liveRegionElement.innerText = "System error, filtering is unavailable at this time.";
            message = ex.message;
            isValid = false;
        }
        if (liveRegionElement !== null && liveRegionElement.innerText.length > 0)
            setTimeout(() => liveRegionElement.innerText = "", 3000);
        inputElement.setAttribute("aria-invalid", (!isValid).toString().toLowerCase());
    }
    const debouncedText = { TextValue: inputElement.value, IsValid: isValid, ExceptionMessage: message };
    debounceConfiguration.blazorCallBackRef.invokeMethodAsync(debounceConfiguration.callBackName, debouncedText);
}
function handleOnInput(event) {
    if (!(event.target instanceof HTMLInputElement))
        return;
    const inputElement = event.target;
    const mapEntry = _handlerMap.get(inputElement);
    if (!mapEntry)
        return;
    const { configuration } = mapEntry;
    if (mapEntry.timer)
        clearTimeout(mapEntry.timer);
    mapEntry.timer = setTimeout(raiseDebounceInterval, configuration.delayMs, inputElement, configuration);
}
function registerTextDebounce(inputElement, debounceConfiguration) {
    if (!inputElement || _handlerMap.has(inputElement) || !debounceConfiguration)
        return;
    const handler = (event) => handleOnInput(event);
    unRegisterTextDebounce(inputElement);
    _handlerMap.set(inputElement, {
        configuration: debounceConfiguration,
        handler: handler
    });
    inputElement.addEventListener("input", handler);
}
function unRegisterTextDebounce(inputElement) {
    if (!inputElement)
        return;
    const mapEntry = _handlerMap.get(inputElement);
    if (mapEntry) {
        inputElement.removeEventListener("input", mapEntry.handler);
        _handlerMap.delete(inputElement);
    }
}
function clearTextValue(inputElement) {
    if (!inputElement)
        return;
    inputElement.value = "";
    inputElement.removeAttribute("aria-invalid");
}
export { registerTextDebounce, unRegisterTextDebounce, clearTextValue };
//# sourceMappingURL=text-debounce.js.map