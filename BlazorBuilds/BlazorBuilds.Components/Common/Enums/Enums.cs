namespace BlazorBuilds.Components.Common.Enums;

public enum Alignment     : int { Left, Center, Right }
public enum SortDirection : int { NotSorted, Ascending, Descending }

public enum SelectionMode : int { None, Single, Multiple }


//Below enums are from the Blazor Builds Pager video and project
public enum PagerDisplaySize : int { Compact = 0, Medium = 7, Large = 9 }
public enum PagerButtonStyle : int { Square, Round }
public enum PagerNavType     : int { Button, Link }
public enum PagerInfoDisplay : int { None, WithAnnouncement, WithoutAnnouncement }
public enum StyleAs          : int { Dynamic, OnDark, OnLight };
