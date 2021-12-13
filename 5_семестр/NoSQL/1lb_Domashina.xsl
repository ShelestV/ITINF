<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform">
    <xsl:template match="/">
        <html>
            <body>
                <h1>Menu</h1>
                <div>
                    <h3>Pizza with main ingridient Salyami</h3>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Weight</th>
                            <th style="text-align:left">Price</th>
                        </tr>
                        <xsl:for-each
                                select="menu/pizza-map/pizza[@product='Salyami']">
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="weight"/></td>
                                <td><xsl:value-of select="price"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
		        <div>
                    <h3>Pizza with weight equaled 550</h3>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Weight</th>
                            <th style="text-align:left">Price</th>
                        </tr>
                        <xsl:for-each
                                select="menu/pizza-map/pizza[weight=550]">
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="weight"/></td>
                                <td><xsl:value-of select="price"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
                <div>
                    <h2>Pizza sorted by price</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Weight</th>
                            <th style="text-align:left">Price</th>
                        </tr>
                        <xsl:for-each select="menu/pizza-map/pizza">
                            <xsl:sort select="price" data-type="number" order="descending"/>
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                                <td><xsl:value-of select="weight"/></td>
                                <td><xsl:value-of select="price"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
		        <div>
                    <h2>Products sorted by name</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                        </tr>
                        <xsl:for-each select="menu/products/product">
                            <xsl:sort select="name" data-type="text"/>
                            <tr>
                                <td><xsl:value-of select="name"/></td>
                            </tr>
                        </xsl:for-each>
                    </table>
                </div>
                <div>
                    <h2>Pizza with price &lt; 200</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Weight</th>
                            <th style="text-align:left">Price</th>
                        </tr>
                        <xsl:for-each select="menu/pizza-map/pizza">
                            <xsl:if test="price &lt; 200">
                                <tr>
                                    <td><xsl:value-of select="name"/></td>
                                    <td><xsl:value-of select="weight"/></td>
                                    <td><xsl:value-of select="price"/></td>
                                </tr>
                            </xsl:if>
                        </xsl:for-each>
                    </table>
                </div>
		        <div>
                    <h2>Pizza with weight &gt; 550</h2>
                    <table border="1">
                        <tr bgcolor="#9acd32">
                            <th style="text-align:left">Name</th>
                            <th style="text-align:left">Weight</th>
                            <th style="text-align:left">Price</th>
                        </tr>
                        <xsl:for-each select="menu/pizza-map/pizza">
                            <xsl:if test="weight &gt; 550">
                                <tr>
                                    <td><xsl:value-of select="name"/></td>
                                    <td><xsl:value-of select="weight"/></td>
                                    <td><xsl:value-of select="price"/></td>
                                </tr>
                            </xsl:if>
                        </xsl:for-each>
                    </table>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
