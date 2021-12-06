<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
            <body>
                <h1>Gun system</h1>
                <div>
                    <h3>Coach gun owners</h3>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Last name</th>
                            <th style="text-align:left">Age</th>
                            <th style="text-align:left">National</th>
                        </tr>
                        <xsl:for-each
                                select="gun-system/gun-owners/gun-owner[@gun='Coach gun']">
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="last-name"/></td>
                                <td><xsl:value-of select="age"/></td>
                                <td><xsl:value-of select="national"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
		        <div>
                    <h3>Winchester Model 1887/1901</h3>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Last name</th>
                            <th style="text-align:left">Age</th>
                            <th style="text-align:left">National</th>
                        </tr>
                        <xsl:for-each
                                select="gun-system/gun-owners/gun-owner[@gun='Winchester Model 1887/1901']">
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="last-name"/></td>
                                <td><xsl:value-of select="age"/></td>
                                <td><xsl:value-of select="national"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
                <div>
                    <h2>Guns sorted by length</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Length</th>
                            <th style="text-align:left">Caliber</th>
                        </tr>
                        <xsl:for-each select="gun-system/guns/gun">
                            <xsl:sort select="length" data-type="number" order="descending"/>
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="length"/></td>
                                <td><xsl:value-of select="caliber"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
		        <div>
                    <h2>Guns sorted by name</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Length</th>
                            <th style="text-align:left">Caliber</th>
                        </tr>
                        <xsl:for-each select="gun-system/guns/gun">
                            <xsl:sort select="name" data-type="text"/>
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="length"/></td>
                                <td><xsl:value-of select="caliber"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
                <div>
                    <h2>Guns with caliber &lt; 12</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Length</th>
                            <th style="text-align:left">Caliber</th>
                        </tr>
                        <xsl:for-each select="gun-system/guns/gun">
                            <xsl:if test="caliber &lt; 12">
                                <tr>
                                    <td><xsl:value-of select="name"/></td>
                                    <td><xsl:value-of select="length"/></td>
                                    <td><xsl:value-of select="caliber"/></td>
                                </tr>
                            </xsl:if>
                        </xsl:for-each>
                    </table>
                </div>
		        <div>
                    <h2>Guns with length &gt; 900</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Length</th>
                            <th style="text-align:left">Caliber</th>
                        </tr>
                        <xsl:for-each select="gun-system/guns/gun">
                            <xsl:if test="length &gt; 900">
                                <tr>
                                    <td><xsl:value-of select="name"/></td>
                                    <td><xsl:value-of select="length"/></td>
                                    <td><xsl:value-of select="caliber"/></td>
                                </tr>
                            </xsl:if>
                        </xsl:for-each>
                    </table>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
