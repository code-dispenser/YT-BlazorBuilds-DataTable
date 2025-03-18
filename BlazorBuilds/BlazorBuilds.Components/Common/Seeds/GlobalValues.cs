using BlazorBuilds.Components.Common.Enums;

namespace BlazorBuilds.Components.Common.Seeds;

public class GlobalValues
{
    public const string DataTable_Data_Property_Exception_Message = "The Data Property cannot be null";

    public const string DataTable_No_Records_Message      = "No entries found.";
    public const string DataTable_Aria_Select_Row_Label   = "Select row";                                                      
    public const string DataTable_Class                   =  "data-table";
    public const string DataTable_Content_Wrapper_Class   = $"{DataTable_Class}__content-wrapper";
    public const string DataTable_Content_Class           = $"{DataTable_Class}__content";
    public const string DataTable_Header_Class            = $"{DataTable_Class}__heading";
    public const string DataTable_Column_Header_Class     = $"{DataTable_Class}__column-header";
    public const string DataTable_Paging_Top_Class        = $"{DataTable_Class}__paging-top";
    public const string DataTable_Paging_Bottom_Class     = $"{DataTable_Class}__paging-bottom";
    public const string DataTable_Icon_Class              = $"{DataTable_Class}__icon";
                                                          
    public const string DataTable_Title_Class             = $"{DataTable_Class}__title";
    public const string DataTable_Title_Hidden_Class      = $"{DataTable_Title_Class}--hidden";
    public const string DataTable_Row_Class               = $"{DataTable_Class}__row";
    public const string DataTable_Row_Selectable_Class    = $"{DataTable_Row_Class}--selectable";
    public const string DataTable_Row_Selected_Class      = $"{DataTable_Row_Class}--selected";
    public const string DataTable_Checkbox_Selector_Class = $"{DataTable_Class}__checkbox-selector";
    public const string DataTable_Header_Row_Class        = $"{DataTable_Class}__header-row";
    public const string DataTable_Row_No_Data_Class       = $"{DataTable_Row_Class}--no-data";

    public const string DataTable_Sortable_Column_Class = $"{DataTable_Class}__sortable-column";
    
    public const string Text_Align_Centre           = "text-align-centre";
    public const string Text_Align_Right            = "text-align-right";
    public const string Text_Align_Left             = "text-align-left";


    public const string DataTable_Sort_UP_Icon_Name_Class    = "sort-asc-icon";
    public const string DataTable_Sort_Down_Icon_Name_Class  = "sort-desc-icon";
    public const string DataTable_Not_Sorted_Icon_Name_Class = "not-sorted-icon ";

    public const string DataTable_Spinner_Backdrop_Class = $"{DataTable_Class}__spinner-backdrop";
    public const string DataTable_Spinner_Class          = $"{DataTable_Class}__spinner";

    public const string Visually_Hidden_Class = "screen-reader-only";

    /*
         * Below are all the values brought in from the Blazor Builds Pager video and project. 
    */
    public const string Aria_Pager_Label_Exception_Message = "The aria pager label is required. It cannot be null, empty or whitespace";

    public const string KeyBoard_Enter_Key         = "Enter";

    public const string Pager_Class                = "pager";
    public const string Pager_Label_Class          = $"{Pager_Class}__label";
    public const string Pager_Input_Class          = $"{Pager_Class}__input";
    public const string Pager_Information_Class    = $"{Pager_Class}__information";
    public const string Pager_Controls_Class       = $"{Pager_Class}__controls";
    public const string Pager_Counter_Class        = $"{Pager_Class}__counter";
    public const string Pager_Buttons_Class        = $"{Pager_Class}__buttons";
    public const string Pager_Button_Class         = $"{Pager_Class}__button";
    public const string Pager_Button_Round_Class   = $"{Pager_Button_Class}--round";
    public const string Pager_Button_Square_Class  = $"{Pager_Button_Class}--square";
    public const string Pager_Icon_Class           = $"{Pager_Class}__icon";
    
    public const string Pager_Next_Label           = "Next";
    public const string Pager_Previous_Label       = "Previous";
    public const string Pager_Aria_Ellipse_Label   = "Ellipse, indicating skipped pages";
    public const string Pager_Input_Label          = "Go to page:";
    public const string Pager_Input_Description    = "Enter key to activate.";
    public const string Pager_No_Records_String    = "No entries found.";
    public const string Pager_Count_Format_String  = "Page {0}, entries {1} to {2} of {3}";
    public const string Pager_Filter_Count_String  = "(filtered from {0} total entries)";
    public const string Pager_Next_Icon_Name_Class = "navigate-next-icon";
    public const string Pager_Prev_Icon_Name_Class = "navigate-previous-icon";

    /*
        * Below are the values brought in from the Blazor builds Debounce filter video and project 
    */

    public const string JavaScript_Clear_Text_Func          =  "clearTextValue";
    public const string JavaScript_Register_Func            = "registerTextDebounce";
    public const string JavaScript_UnRegister_Func          = "unRegisterTextDebounce";
    public const string JavaScript_File_Path                = "./_content/BlazorBuilds.Components/assets/js/text-debounce.js";
                                                            
    public const string LabelText_Exception_Message         = "The label text parameter is required. It cannot be null, empty or whitespace";
    public const string HintText_Exception_Message          = "The hint text parameter is required. It cannot be null, empty or whitespace";

    public const string TextDebounce_Class                  = "text-debounce";
    public const string TextDebounce_Text_Input_Class       = $"{TextDebounce_Class}__text-input";
    public const string TextDebounce_Hint_Content_Class     = $"{TextDebounce_Class}__hint-content";
    public const string TextDebounce_Hint_Text_Class        = $"{TextDebounce_Class}__hint-text";
    public const string TextDebounce_Hint_Text_Hidden_Class = $"{TextDebounce_Hint_Text_Class}--hidden";
    public const string TextDebounce_Label_Class            = $"{TextDebounce_Class}__label";
    public const string TextDebounce_Icon_Class             = $"{TextDebounce_Class}__icon";
    public const string TextDebounce_State_Icon_Class       = $"{TextDebounce_Class}__state-icon";
    public const string TextDebounce_SR_Only_Class          = $"{TextDebounce_Class}__screen-reader-only";


    public const string TextDebounce_Aria_Invalid_Text      = "Invalid entry";

    public const string TextDebounce_Regex_Pattern          = @"^(?=.{0,25}$)([A-Za-z0-9]+(['\- ][A-Za-z0-9]+)*)?$";//a to z, 0 to 9, single apostrophe's, spaces and dashes max 25 characters or empty to clear filter.
    public const int    TextBounce_DelayMs                  = 300;



    public const string Style_As_Dark  = "dark";
    public const string Style_As_Light = "light";

    public static string? GetStyleAsValue(StyleAs styleAs)

    => styleAs switch
    {
        StyleAs.OnLight => Style_As_Light,
        StyleAs.OnDark => Style_As_Dark,
        _ => null
    };


}
