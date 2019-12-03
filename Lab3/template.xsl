<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" version="1.0">
<xsl:template match="menu">

<HTML>

<BODY>

<TABLE BORDER="2">

<THEAD>

<TR>

<TH>

<b>Name</b>

</TH>

<TH>

<b>Description</b>

</TH>

<TH>

<b>Mealtime</b>

</TH>

<TH>

<b>Time of presentation</b>

</TH>

<TH>

<b>Calories</b>

</TH>

<TH>

<b>Price</b>

</TH>

</TR>

</THEAD>

<TBODY>

<xsl:for-each select="/*/dish">

<TR>

<TD>

<b><xsl:value-of select="./name" /></b>

</TD>

<TD>

<b><xsl:value-of select="./description" /></b>

</TD>

<TD>

<b><xsl:value-of select="./mealTime" /></b>

</TD>

<TD>

<b><xsl:value-of select="./presentationTime" /></b>

</TD>

<TD>

<b><xsl:value-of select="./calories" /></b>

</TD>

<TD>

<b><xsl:value-of select="./price" /></b>


</TD>

</TR>

</xsl:for-each>

</TBODY>

</TABLE>

</BODY>

</HTML>

</xsl:template>
</xsl:stylesheet>